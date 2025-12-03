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
            Id: _appUserInfo.Id, // Fallback ID for static data
            FirstName: $('#firstName').val(),
            LastName: $('#lastName').val(),
            Email: $('#email').val(),
            Phone: $('#phone').val()
        };

        // For demo purposes - simulate API call
        console.log('Updating profile:', profileData);

        // Simulate API call with timeout
        setTimeout(function () {
            // Update stored user info if available
            if (_appUserInfo) {
                _appUserInfo.FirstName = profileData.FirstName;
                _appUserInfo.LastName = profileData.LastName;
                _appUserInfo.Email = profileData.Email;
                _appUserInfo.Phone = profileData.Phone;
                storageService.set('ApplicationUser', _appUserInfo);
            }

            // Show success message
            alert('Profile updated successfully!');

            // Remove validation styles
            $('#profileForm').removeClass('was-validated');
        }, 1000);
    };

    self.changePassword = function () {
        var _appUserInfo = self.ApplictionUser;
        var passwordData = {
            UserId: _appUserInfo ? _appUserInfo.Id : 1, // Fallback ID for static data
            CurrentPassword: $('#currentPassword').val(),
            NewPassword: $('#newPassword').val()
        };

        // For demo purposes - simulate API call
        console.log('Changing password for user:', passwordData.UserId);

        // Simulate API call with timeout
        setTimeout(function () {
            // Clear password fields
            $('#currentPassword').val('');
            $('#newPassword').val('');
            $('#confirmPassword').val('');
            $('#passwordForm').removeClass('was-validated');

            // Show success message
            alert('Password changed successfully!');
        }, 1000);
    };
}