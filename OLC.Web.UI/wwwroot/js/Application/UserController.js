function UserController() {

    $(".se-pre-con").show();

    var self = this;

    self.usersList = [];

    self.currentUser = {};

    self.rolesList = rolesList.filter(x => x.IsActive == true);

    self.init = function () {

        var appUserInfo = storageService.get('ApplicationUser');

        if (appUserInfo) {
            self.currentUser = appUserInfo;
        }

        var form = $('#CreatePortalUserForm');
        var signUpButton = $('#btnSubmit');
        form.on('input', 'input, select, textarea', checkFormValidity);
        checkFormValidity();
        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }

        GetUserAccountsAsync();
        function GetUserAccountsAsync() {
            makeAjaxRequest({
                url: API_URLS.GetUserAccountsAsync,
                type: 'GET',
                successCallback: handleUserAccountsSuccess,
                errorCallback: handleUserAccountsError
            });
        }

       
        function handleUserAccountsSuccess(response) {

            console.info(response);

            self.usersList = response && response.data ? response.data : [];

            genarateDropdown("RoleId", self.rolesList, "Id", "Name");

            populateUserTable();

            $(".se-pre-con").hide();
        }

        function handleUserAccountsError(xhr, status, error) {
            console.error('Error loading user data:', error);
            $('#userAccountsBody').html(`
                <tr>
                    <td colspan="8" class="text-center text-danger">
                        Error loading user data. Please try again.
                    </td>
                </tr>
            `);
            $(".se-pre-con").hide();
        }

        // Function to format date
        function formatDate(dateString) {
            const date = new Date(dateString);
            return date.toLocaleDateString('en-US', {
                year: 'numeric',
                month: 'short',
                day: 'numeric'
            });
        }

        // Function to get status badge
        function getStatusBadge(isActive, isBlocked) {
            if (isBlocked) {
                return '<span class="badge bg-danger status-badge">Blocked</span>';
            } else if (isActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return '<span class="badge bg-warning status-badge">Inactive</span>';
            }
        }
        function getExternalBadge(isExternal) {
            if (isExternal) {
                return '<span class="badge bg-success status-badge">Yes</span>';
            } else {
                return '<span class="badge bg-warning status-badge">No</span>';
            }
        }

        // Function to get role name based on RoleId
        function getRoleName(roleId) {
            const roles = {
                1: 'Admin',
                2: 'User',
                3: 'Manager',
                4: 'Viewer'
            };
            return roles[roleId] || 'Unknown';
        }

        // Populate the table with user data
        function populateUserTable() {
            const tbody = $('#userAccountsBody');
            const cardsContainer = $('#mobileUserAccountsCards');
            tbody.empty(); // Clear existing desktop table rows
            cardsContainer.empty(); // Clear existing mobile cards

            if (self.usersList.length === 0) {
                tbody.append(`<tr>
            <td colspan="9" class="text-center text-muted">No user accounts found</td>
            </tr>`);
                cardsContainer.append(`<div class="text-center text-muted p-4">No user accounts found</div>`);
                return;
            }

            self.usersList.forEach(function (user) {
                const fullName = user.FirstName + ' ' + user.LastName;
                const statusBadge = getStatusBadge(user.IsActive, user.IsBlocked);
                const roleName = getRoleName(user.RoleId);
                const externalUser = getExternalBadge(user.IsExternalUser);
                const lastUpdated = formatDate(user.ModifiedOn);

                // Check if user is admin
                const isAdmin = roleName.toLowerCase() === 'admin';

                // Generate action buttons based on role
                const actionButtons = `
        <button class="btn btn-sm btn-outline-primary view-user" data-user-id="${user.Id}" data-user='${JSON.stringify(user).replace(/'/g, "&apos;")}' title="view user profile">
            <i class="fas fa-eye"></i>
        </button>`;

                // Desktop table row
                const row = `
        <tr class="user-account-item" data-user='${JSON.stringify(user).replace(/'/g, "&apos;")}' data-user-id='${user.Id}' data-is-admin='${isAdmin}'>
            <td><a style="cursor:pointer;color:blue;" class="view-user" data-user-id="${user.Id}">#${user.Id}</a></td>
            <td>${fullName}</td>
            <td>${user.Email}</td>
            <td>${user.Phone || 'N/A'}</td>
            <td>${roleName}</td>
            <td>${externalUser}</td>
            <td>${statusBadge}</td>
            <td>${lastUpdated}</td>
            <td>
                ${actionButtons}
            </td>
        </tr>
    `;
                tbody.append(row);

                // Mobile card
                const cardHtml = `
        <div class="card mb-3 border" data-user='${JSON.stringify(user).replace(/'/g, "&apos;")}' data-user-id='${user.Id}' data-is-admin='${isAdmin}'>
            <div class="card-header bg-light d-flex justify-content-between align-items-center">
                <strong>${fullName}</strong>
                <span class="badge bg-primary">#${user.Id}</span>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-6 mb-2">
                        <small class="text-muted">Email:</small>
                        <div>${user.Email}</div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">Phone:</small>
                        <div>${user.Phone || 'N/A'}</div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">Role:</small>
                        <div>${roleName}</div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">External:</small>
                        <div>${externalUser}</div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">Status:</small>
                        <div>${statusBadge}</div>
                    </div>
                    <div class="col-6 mb-2">
                        <small class="text-muted">Last Updated:</small>
                        <div>${lastUpdated}</div>
                    </div>
                </div>
            </div>
            <div class="card-footer d-flex justify-content-between">
                ${actionButtons}
            </div>
        </div>
    `;
                cardsContainer.append(cardHtml);
            });
        }

        // Action functions

        $(document).on("click", ".view-user", function () {
            var currentUserId = $(this).data("user-id");
            console.log("current user is .." + currentUserId);

            // Replace "YourController" with your actual controller name (without "Controller")

            var isAdmin = self.currentUser.RoleId == 1 ? true : false;

            window.location.href = '/User/ManageUser?userId=' + currentUserId + '&isReadOnly=' + isAdmin + '';
        });
       
        $(document).on("click", "#addUserBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#CreatePortalUserForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $('#CreatePortalUserForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

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
                    $('#AddEditBankForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetUserAccountsAsync();
                },
                errorCallback: function (error) {
                    console.log(error);
                }
            });
        });

    }
}