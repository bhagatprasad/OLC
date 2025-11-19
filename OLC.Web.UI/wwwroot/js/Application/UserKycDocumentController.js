function UserKycDocumentController() {
    var self = this;
    self.init = function () {
        // Get user info from storage
        var _appUserInfo = storageService.get('ApplicationUser');

        // Check if user info exists and KYC status is Pending or InProgress (NOT Submitted)
        if (_appUserInfo &&
            (_appUserInfo.KycStatus === 'Pending' || _appUserInfo.KycStatus === 'In Progress') &&
            _appUserInfo.KycStatus !== 'Submitted' && // Explicitly exclude Submitted status
            !sessionStorage.getItem('kycModalShown')) {

            // Show modal after a short delay
            setTimeout(function () {
                $('#kycModal').modal({ backdrop: 'static', keyboard: false });
                $('#kycModal').modal('show');
            }, 1000);

            // Set flag when modal is closed
            $('#kycModal').on('hidden.bs.modal', function () {
                sessionStorage.setItem('kycModalShown', 'true');
            });

            $('.btn-close').on('click', function () {
                sessionStorage.setItem('kycModalShown', 'true');
                $('#kycModal').modal('hide');
            });
        }

        // Step Navigation
        $('#startKycBtn').on('click', function () {
            $('#kycWarningStep').hide();
            $('#kycUploadStep').show();
        });

        $('#backToWarningBtn').on('click', function () {
            $('#kycUploadStep').hide();
            $('#kycWarningStep').show();
        });

        // File Upload Handler
        $('#kycUploadForm').on('submit', function (e) {
            var _appUserInfo = storageService.get('ApplicationUser');
            e.preventDefault();

            var formData = new FormData();
            var fileInput = document.getElementById('documentFile');
            var file = fileInput.files[0];

            if (!file) {
                alert('Please select a document to upload.');
                return;
            }

            // Validate file size (5MB)
            if (file.size > 5 * 1024 * 1024) {
                alert('File size must be less than 5MB.');
                return;
            }

            // Show progress bar
            $('#uploadProgress').show();
            $('.progress-bar').css('width', '30%');

            // Read file and convert to base64
            var reader = new FileReader();
            reader.onload = function (event) {
                var base64String = event.target.result.split(',')[1]; // Remove data URL prefix

                // Prepare the data for API
                var kycDocument = {
                    DocumentType: $('#documentType').val(),
                    DocumentNumber: $('#documentNumber').val(),
                    DocumentFileData: base64String,
                    UserId: _appUserInfo.Id
                };

                $('.progress-bar').css('width', '60%');

                // Send to server
                $.ajax({
                    url: '/User/UploadAndUpdateUserKycDocument',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(kycDocument),
                    success: function (response) {
                        $('.progress-bar').css('width', '100%');

                        setTimeout(function () {
                            if (response) {
                                // Success
                                $('#kycModal').modal('hide');
                                sessionStorage.setItem('kycModalShown', 'true');

                                if (_appUserInfo) {
                                    storageService.remove('ApplicationUser');
                                }
                                storageService.set('ApplicationUser', response.data);
                                location.reload();
                            } else {
                                alert('Failed to upload KYC document. Please try again.');
                            }
                        }, 500);
                    },
                    error: function (xhr, status, error) {
                        $('.progress-bar').css('width', '0%');
                        $('#uploadProgress').hide();
                        alert('Error uploading document: ' + error);
                    }
                });
            };

            reader.onerror = function () {
                $('#uploadProgress').hide();
                alert('Error reading file. Please try again.');
            };

            reader.readAsDataURL(file);
        });
    }
}