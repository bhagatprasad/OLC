function ProfileController() {
    var self = this;

    self.ApplictionUser = {};

    self.init = function () {
        self.loadUserProfile();
        self.loadWalletData();
        self.initFormHandlers();
    };

    self.loadUserProfile = function () {
        self.ApplictionUser = storageService.get('ApplicationUser');
        self.loadStaticProfileData(self.ApplictionUser);
      
    };

    self.loadStaticProfileData = function (applicationUser) {
        // Static profile data
        var staticProfileData = {
            FirstName: applicationUser.FirstName,
            LastName: applicationUser.LastName,
            Email: applicationUser.Email,
            Phone: applicationUser.Phone
        };
        self.bindProfileData(staticProfileData);
    };

    self.loadWalletData = function () {
       
        if (self.ApplictionUser && self.ApplictionUser.Id) {
            // Load single wallet data for user
            $.ajax({
                url: '/UserWallet/GetUserWallet',
                type: 'GET',
                data: { userId: self.ApplictionUser.Id },
                success: function (response) {
                    if (response && response.data) {
                        self.bindWalletData(response.data);
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error loading wallet data:', error);
                    // Show static wallet data
                    self.loadStaticWalletData();
                }
            });
        }
    };

    self.loadStaticWalletData = function () {
        // Static wallet data
        var staticWalletData = {
            WalletId: 'WLT001',
            WalletType: 'cash',
            CurrentBalance: 1500.75,
            TotalEarned: 2500.00,
            TotalSpent: 999.25,
            Currency: 'INR',
            IsActive: true,
            ModifiedOn: new Date().toISOString()
        };
        self.bindWalletData(staticWalletData);
    };

    self.bindProfileData = function (profileData) {
        // Bind data to profile form
        $('#firstName').val(profileData.FirstName || '');
        $('#lastName').val(profileData.LastName || '');
        $('#email').val(profileData.Email || '');
        $('#phone').val(profileData.Phone || '');
    };

    self.bindWalletData = function (walletData) {
        // Hide loading spinner
        $('#walletLoading').hide();

        if (walletData) {
            // Show wallet data
            $('#walletData').show();

            // Update status badge
            const statusBadge = walletData.IsActive
                ? '<span class="badge bg-success">Active</span>'
                : '<span class="badge bg-danger">Inactive</span>';
            $('#walletStatusBadge').html(statusBadge);

            // Format currency
            const currencySymbol = walletData.Currency === 'INR' ? '₹' : '$';

            // Update card values
            $('#walletId').text(walletData.WalletId || 'N/A');
            $('#currentBalance').text(`${currencySymbol}${(walletData.CurrentBalance || 0).toFixed(2)}`);
            $('#totalEarned').text(`${currencySymbol}${(walletData.TotalEarned || 0).toFixed(2)}`);
            $('#totalSpent').text(`${currencySymbol}${(walletData.TotalSpent || 0).toFixed(2)}`);

            // Update additional info
            const walletType = walletData.WalletType ?
                walletData.WalletType.charAt(0).toUpperCase() + walletData.WalletType.slice(1) :
                'N/A';
            $('#walletType').text(walletType);
            $('#currency').text(walletData.Currency || 'INR');

            // Format last updated date
            if (walletData.ModifiedOn) {
                const lastUpdated = new Date(walletData.ModifiedOn).toLocaleString();
                $('#lastUpdated').text(lastUpdated);
            } else {
                $('#lastUpdated').text('Just now');
            }
        } else {
            // Show error state
            $('#walletError').show();
        }
    };

    self.initFormHandlers = function () {
        // Profile form submission
        $('#profileForm').on('submit', function (e) {
            e.preventDefault();
            self.updateProfile();
        });

        // Password form submission
        $('#passwordForm').on('submit', function (e) {
            e.preventDefault();
            self.changePassword();
        });

        // Form validation
        self.initFormValidation();
    };

    self.initFormValidation = function () {
        // Bootstrap form validation
        (function () {
            'use strict';
            window.addEventListener('load', function () {
                var forms = document.getElementsByClassName('needs-validation');
                var validation = Array.prototype.filter.call(forms, function (form) {
                    form.addEventListener('submit', function (event) {
                        if (form.checkValidity() === false) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        form.classList.add('was-validated');
                    }, false);
                });
            }, false);
        })();

        // Password confirmation validation
        $('#passwordForm').on('submit', function (e) {
            var newPassword = $('#newPassword').val();
            var confirmPassword = $('#confirmPassword').val();

            if (newPassword !== confirmPassword) {
                e.preventDefault();
                $('#confirmPassword').addClass('is-invalid');
                $('#confirmPassword').siblings('.invalid-feedback').text('Passwords do not match.');
            } else {
                $('#confirmPassword').removeClass('is-invalid');
            }
        });
    };

    self.updateProfile = function () {

        var _appUserInfo = self.ApplictionUser;

        var profileData = {
            Id: _appUserInfo.Id,
            FirstName: $('#firstName').val(),
            LastName: $('#lastName').val(),
            Email: $('#email').val(),
            Phone: $('#phone').val()
        };

        console.log("Sending Update Profile Request:", profileData);

        $.ajax({
            url: "/User/UpdateUserPersonalInformation",
            type: "POST",
            data: JSON.stringify(profileData),
            contentType: "application/json",

            success: function (response) {

               
                console.log("RAW RESPONSE:", JSON.stringify(response));

                if (response && response.data) {

                    var updatedUser = response.data;

                    _appUserInfo.FirstName = updatedUser.firstName;
                    _appUserInfo.LastName = updatedUser.lastName;
                    _appUserInfo.Email = updatedUser.email;
                    _appUserInfo.Phone = updatedUser.phone;

                    storageService.set('ApplicationUser', updatedUser);
                    self.ApplictionUser = updatedUser;

                    self.loadUserProfile();

                    alert("Profile updated successfully!");
                }
                else {
                    alert("Failed to update profile.");
                }
            },

            error: function (xhr) {
                console.error("Error updating profile:", xhr.responseText);
                alert("Error occurred while updating profile.");
            }
        });
    };

    

    self.changePassword = function () {

        var _appUserInfo = self.ApplictionUser;

        var passwordData = {
            UserId: _appUserInfo ? _appUserInfo.Id : 0,
            Password: $("#newPassword").val(),
            ConformPassword: $("#confirmPassword").val()
        };

        console.log("Sending Change Password Request:", passwordData);

        $.ajax({
            url: "/Account/ChangePassword",
            type: "POST",
            data: JSON.stringify(passwordData),
            contentType: "application/json; charset=utf-8",
            success: function (response) {

                console.log("Change Password Response:", response);

                if (response.data === true) {

                    alert("Password changed successfully!");

                    $("#newPassword").val("");
                    $("#confirmPassword").val("");
                    $("#passwordForm").removeClass("was-validated");
                }
                else {
                    alert("Unable to change password. Please try again.");
                }
            },
            error: function (xhr) {
                console.error("Error:", xhr.responseText);
                alert("Error changing password.");
            }
        });
    };

}