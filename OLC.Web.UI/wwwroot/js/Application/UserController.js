function UserController() {

    var self = this;

    self.usersList = [];

    self.init = function () {
        makeAjaxRequest({
            url: API_URLS.GetUserAccountsAsync,
            type: 'GET',
            successCallback: handleUserAccountsSuccess,
            errorCallback: handleUserAccountsError
        });
        function handleUserAccountsSuccess(response) {
            console.info(response);

            self.usersList = response && response.data ? response.data : [];

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
            tbody.empty(); // Clear existing rows

            if (self.usersList.length === 0) {
                tbody.append(`<tr>
                    <td colspan="8" class="text-center text-muted">No user accounts found</td>
                    </tr>`);
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
                const actionButtons = isAdmin
                    ? `
                <button class="btn btn-sm btn-outline-primary view-user" data-user-id="${user.Id}" data-user='${user}'>
                    <i class="fas fa-eye"></i>
                </button>`
                    : `
                <button class="btn btn-sm btn-outline-primary view-user" data-user-id="${user.Id}" data-user='${user}'>
                    <i class="fas fa-eye"></i>
                </button>
                <button class="btn btn-sm btn-outline-warning edit-user" data-user-id="${user.Id}" data-user='${user}'>
                    <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger delete-user" data-user-id="${user.Id}" data-user='${user}'>
                    <i class="fas fa-trash"></i>
                </button>
            `;

                const row = `
            <tr class="user-account-item" data-user='${user}' data-user-id='${user.Id}' data-is-admin='${isAdmin}'>
                <td>#${user.Id}</td>
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
            });

        }

        // Action functions
        function viewUserDetails(userId) {
            console.log('View user details:', userId);
            // Implement view user details functionality
            alert('View user: ' + userId);
        }

        function editUser(userId) {
            console.log('Edit user:', userId);
            // Implement edit user functionality
            alert('Edit user: ' + userId);
        }

        function deleteUser(userId) {
            console.log('Delete user:', userId);
            if (confirm('Are you sure you want to delete this user?')) {
                // Implement delete user functionality
                alert('Delete user: ' + userId);
            }
        }

    }
}