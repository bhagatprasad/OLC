function UserController() {
    var self = this;
    self.usersList = [];
    self.filteredUsersList = [];
    self.CoreStatus = [];
    self.ApplicationUser = {};
    self.rolesList = rolesList;
    self.dropDownRolesList = rolesList.filter(x => x.IsActive == true);
    self.CurrentSelectedUser = {};
    var actions = [];

    self.init = function () {
        $(".se-pre-con").show();

        var appUserInfo = storageService.get('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        actions.push("/User/GetUserAccounts");

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log(responses);
            self.usersList = responses[0] && responses[0].data ? responses[0].data : [];
            self.filteredUsersList = [...self.usersList];
            genarateDropdown("RoleId", self.dropDownRolesList, "Id", "Name");
            self.populateSummaryCards();
            self.populateUserAccountsGrid();
            self.initializeSearch();
            self.initializeEventHandlers();

            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.log('One or more requests failed:', error);
            $(".se-pre-con").hide();
        });
    };

    // Populate summary cards with calculated data
    self.populateSummaryCards = function () {
        if (self.usersList.length === 0) return;

        const totalUsers = self.usersList.length;
        const activeUsers = self.usersList.filter(user => user.IsActive && !user.IsBlocked).length;
        const blockedUsers = self.usersList.filter(user => user.IsBlocked).length;
        const externalUsers = self.usersList.filter(user => user.IsExternalUser).length;

        $('#totalUsers').text(totalUsers.toLocaleString());
        $('#activeUsers').text(activeUsers.toLocaleString());
        $('#blockedUsers').text(blockedUsers.toLocaleString());
        $('#externalUsers').text(externalUsers.toLocaleString());

        // Calculate percentage changes (mock data for demonstration)
        $('#usersChange').text('5%');
        $('#activeChange').text('8%');
        $('#blockedChange').text('-2%');
        $('#externalChange').text('12%');
    };

    // Initialize search functionality
    self.initializeSearch = function () {
        $('#userSearch').on('input', function () {
            self.performUserSearch($(this).val());
        });
    };

    // Perform search across users
    self.performUserSearch = function (searchTerm) {
        if (!searchTerm || searchTerm.trim() === '') {
            self.filteredUsersList = [...self.usersList];
        } else {
            const term = searchTerm.toLowerCase().trim();
            self.filteredUsersList = self.usersList.filter(user =>
                (user.FirstName && user.FirstName.toLowerCase().includes(term)) ||
                (user.LastName && user.LastName.toLowerCase().includes(term)) ||
                (user.Email && user.Email.toLowerCase().includes(term)) ||
                (user.Phone && user.Phone.includes(term)) ||
                (self.getRoleName(user.RoleId).toLowerCase().includes(term)) ||
                (user.Id && user.Id.toString().includes(term))
            );
        }
        self.populateUserAccountsGrid();
    };

    // Function to format date
    self.formatDate = function (dateString) {
        if (!dateString) return 'N/A';
        const date = new Date(dateString);
        return date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    };

    // Function to get status badge
    self.getStatusBadge = function (isActive, isBlocked) {
        if (isBlocked) {
            return '<span class="badge bg-danger status-badge">Blocked</span>';
        } else if (isActive) {
            return '<span class="badge bg-success status-badge">Active</span>';
        } else {
            return '<span class="badge bg-warning status-badge">Inactive</span>';
        }
    };

    // Function to get external user badge
    self.getExternalBadge = function (isExternal) {
        if (isExternal) {
            return '<span class="badge bg-info status-badge">Yes</span>';
        } else {
            return '<span class="badge bg-secondary status-badge">No</span>';
        }
    };

    // Function to get role name based on RoleId
    self.getRoleName = function (roleId) {
        const role = self.rolesList.find(r => r.Id === roleId);
        return role ? role.Name : 'Unknown';
    };

    // Truncate long text for better display
    self.truncateText = function (text, maxLength) {
        if (!text) return 'N/A';
        if (text.length <= maxLength) return text;
        return text.substring(0, maxLength) + '...';
    };

    // Populate user accounts grid
    self.populateUserAccountsGrid = function () {
        const tbody = $('#userAccountsBody');
        const cardsContainer = $('#mobileUserAccountsCards');
        tbody.empty();
        cardsContainer.empty();

        if (self.filteredUsersList.length === 0) {
            tbody.append(`
                <tr>
                    <td colspan="9" class="text-center text-muted">
                        ${self.usersList.length === 0 ? 'No user accounts found' : 'No users match your search'}
                    </td>
                </tr>
            `);
            cardsContainer.append(`
                <div class="text-center text-muted p-4">
                    ${self.usersList.length === 0 ? 'No user accounts found' : 'No users match your search'}
                </div>
            `);
            return;
        }

        self.filteredUsersList.forEach(function (user) {
            const fullName = (user.FirstName + ' ' + user.LastName).trim();
            const statusBadge = self.getStatusBadge(user.IsActive, user.IsBlocked);
            const roleName = self.getRoleName(user.RoleId);
            const externalUser = self.getExternalBadge(user.IsExternalUser);
            const lastUpdated = self.formatDate(user.ModifiedOn);

            // KYC Status with colors and icons
            const kycStatus = user.KycStatus || 'N/A';
            let kycBadgeClass = 'bg-secondary'; // default
            let kycIcon = '';

            switch (kycStatus.toLowerCase()) {
                case 'pending':
                    kycBadgeClass = 'bg-warning text-dark';
                    kycIcon = '<i class="fas fa-clock me-1"></i>';
                    break;
                case 'completed':
                    kycBadgeClass = 'bg-success';
                    kycIcon = '<i class="fas fa-check me-1"></i>';
                    break;
                case 'in progress':
                    kycBadgeClass = 'bg-info';
                    kycIcon = '<i class="fas fa-sync-alt me-1"></i>';
                    break;
                default:
                    kycBadgeClass = 'bg-secondary';
                    kycIcon = '';
            }

            const kycBadge = `<span class="badge ${kycBadgeClass} kyc-status-badge">${kycIcon}${kycStatus}</span>`;

            // Action buttons - show assign icon only for pending KYC status
            const isPendingKYC = kycStatus.toLowerCase() === 'pending';
            const assignButton = isPendingKYC && user.IsActive == true && user.IsBlocked == false ?
                `<button class="btn btn-sm btn-outline-success assign-kyc" data-user-id="${user.Id}" title="Assign KYC">
            <i class="fas fa-user-check"></i>
        </button>` : '';

            // Desktop table row
            const row = `
        <tr class="user-account-item" data-user-id="${user.Id}">
            <td>
                <strong class="text-primary">#${user.Id}</strong>
            </td>
            <td class="fw-bold">${fullName || 'N/A'}</td>
            <td>
                <div class="small">
                    <i class="fas fa-envelope me-1 text-muted"></i>${user.Email || 'N/A'}
                </div>
            </td>
            <td>
                <div class="small">
                    <i class="fas fa-phone me-1 text-muted"></i>${user.Phone || 'N/A'}
                </div>
            </td>
            <td>
                <span class="badge bg-primary status-badge">${roleName}</span>
            </td>
            <td>${externalUser}</td>
            <td>${statusBadge}</td>
            <td>${kycBadge}</td>
            <td>
                <div class="action-buttons">
                    ${assignButton}
                    <button class="btn btn-sm btn-outline-primary view-user" data-user-id="${user.Id}" title="View User Details">
                        <i class="fas fa-eye"></i>
                    </button>
                    <button class="btn btn-sm btn-outline-warning edit-user" data-user-id="${user.Id}" title="Edit User">
                        <i class="fas fa-edit"></i>
                    </button>
                </div>
            </td>
        </tr>
    `;
            tbody.append(row);

            // Mobile card - also add assign button for pending KYC
            const mobileAssignButton = isPendingKYC ?
                `<button class="btn btn-sm btn-outline-success flex-fill assign-kyc" data-user-id="${user.Id}">
            <i class="fas fa-user-check me-1"></i>Assign
        </button>` : '';

            const cardHtml = `
        <div class="card mb-3 border" data-user-id="${user.Id}">
            <div class="card-header bg-light d-flex justify-content-between align-items-center">
                <div>
                    <strong class="text-primary">${fullName || 'N/A'}</strong>
                    <br>
                    <small class="text-muted">#${user.Id}</small>
                </div>
                <div class="d-flex align-items-center gap-2">
                    ${kycBadge}
                    ${statusBadge}
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-6 mb-2">
                        <small class="text-muted">Email:</small>
                        <div><i class="fas fa-envelope me-1 text-muted"></i>${user.Email || 'N/A'}</div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">Phone:</small>
                        <div><i class="fas fa-phone me-1 text-muted"></i>${user.Phone || 'N/A'}</div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">Role:</small>
                        <div><span class="badge bg-primary">${roleName}</span></div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">External:</small>
                        <div>${externalUser}</div>
                    </div>
                    <div class="col-12 mb-2">
                        <small class="text-muted">Last Updated:</small>
                        <div><small class="text-muted">${lastUpdated}</small></div>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="d-flex gap-2">
                    ${mobileAssignButton}
                    <button class="btn btn-sm btn-outline-primary flex-fill view-user" data-user-id="${user.Id}">
                        <i class="fas fa-eye me-1"></i>View
                    </button>
                    <button class="btn btn-sm btn-outline-warning flex-fill edit-user" data-user-id="${user.Id}">
                        <i class="fas fa-edit me-1"></i>Edit
                    </button>
                </div>
            </div>
        </div>
    `;
            cardsContainer.append(cardHtml);
        });

        // Initialize click handlers
        self.initializeUserActionHandlers();
    };

    // Initialize user action click handlers
    self.initializeUserActionHandlers = function () {
        $('.view-user').off('click').on('click', function () {
            const userId = $(this).data('user-id');
            self.viewUserDetails(userId);
        });

        $('.edit-user').off('click').on('click', function () {
            const userId = $(this).data('user-id');
            self.editUser(userId);
        });
    };
    $(document).on('click', '.assign-kyc', function () {
        $('#initKycModal').modal({ backdrop: 'static', keyboard: false });
        const userId = $(this).data('user-id');
        const user = self.usersList.find(u => u.Id === userId);
        self.CurrentSelectedUser = user;
        $("#initKycModal").modal("show");
    });
    

    // View user details
    self.viewUserDetails = function (userId) {
        console.log('Viewing user details for:', userId);
        const user = self.usersList.find(u => u.Id === userId);
        if (user) {
            const isAdmin = self.ApplicationUser.RoleId == 1;
            window.location.href = '/User/ManageUser?userId=' + userId + '&isReadOnly=' + isAdmin;
        }
    };

    // Edit user
    self.editUser = function (userId) {
        console.log('Editing user:', userId);
        const user = self.usersList.find(u => u.Id === userId);
        if (user) {
            window.location.href = '/User/ManageUser?userId=' + userId + '&isReadOnly=false';
        }
    };

    // Initialize event handlers for form and buttons
    self.initializeEventHandlers = function () {
        // Add User Button
        $('#addUserBtn').off('click').on('click', function () {
            self.showAddUserForm();
        });

        // Close Sidebar
        $('#closeSidebar').off('click').on('click', function () {
            self.hideAddUserForm();
        });

        // Create User Form Submission
        $('#CreatePortalUserForm').off('submit').on('submit', function (e) {
            e.preventDefault();
            self.createUser();
        });

        // Form validation
        var form = $('#CreatePortalUserForm');
        var signUpButton = $('#btnSubmit');

        form.on('input', 'input, select, textarea', function () {
            self.checkFormValidity(form, signUpButton);
        });

        self.checkFormValidity(form, signUpButton);
    };

    // Check form validity
    self.checkFormValidity = function (form, signUpButton) {
        if (form[0].checkValidity()) {
            signUpButton.prop('disabled', false);
        } else {
            signUpButton.prop('disabled', true);
        }
    };

    // Show add user form
    self.showAddUserForm = function () {
        $('#sidebar').addClass('show');
        $('body').append('<div class="modal-backdrop fade show"></div>');
        console.log("Add user button clicked");
    };

    // Hide add user form
    self.hideAddUserForm = function () {
        $('#CreatePortalUserForm')[0].reset();
        $('#sidebar').removeClass('show');
        $('.modal-backdrop').remove();
    };

    $(document).on("click", ".close-key-init", function () {
        self.CurrentSelectedUser = {};
        $("#initKycModal").modal("hide");
    });
    $(document).on("click", "#confirmInitKycBtn", function () {
        $(".se-pre-con").show();
        var kycLevel = $("#initKycLevel").val();
        var kyc = {
            UserId: self.CurrentSelectedUser.Id,
            KycStatus: 'In Progress',
            KycLevel: kycLevel,
            SubmittedOn: null,
            VerifiedOn: null,
            VerifiedBy: null,
            RejectionReason: ''
        };
        var useKyc = addCommonProperties(kyc);
        makeAjaxRequest({
            url: API_URLS.InsertAndUpdateUserKycAsync,
            data: useKyc,
            type: 'POST',
            successCallback: function (response) {
                self.CurrentSelectedUser = {};
                $("#initKycModal").modal("hide");
                self.init(); 
                $(".se-pre-con").hide();
            },
            errorCallback: function (error) {
                console.log('Error creating user:', error);
                $(".se-pre-con").hide();
            }
        });
    });
    // Create new user
    self.createUser = function () {
        $(".se-pre-con").show();

        var registerUser = {
            FirstName: $("#Firstname").val(),
            LastName: $("#Lastname").val(),
            Email: $("#Email").val(),
            Phone: $("#Phone").val(),
            Password: $("#password").val(),
            RoleId: $("#RoleId").val()
        };

        makeAjaxRequest({
            url: API_URLS.CreatePortalUserAsync,
            data: registerUser,
            type: 'POST',
            successCallback: function (response) {
                self.hideAddUserForm();
                self.init(); // Reload the user data
                $(".se-pre-con").hide();
            },
            errorCallback: function (error) {
                console.log('Error creating user:', error);
                $(".se-pre-con").hide();
            }
        });
    };
}