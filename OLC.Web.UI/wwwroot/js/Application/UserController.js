function UserController() {
    var self = this;
    self.usersList = [];
    self.filteredUsersList = [];
    self.CoreStatus = [];
    self.ApplicationUser = {};
    self.rolesList = rolesList;
    self.dropDownRolesList = rolesList.filter(x => x.IsActive == true);
    self.CurrentSelectedUser = {};
    self.PreviewUserKycDocument = {};
    var actions = [];
    let currentZoom = 1;
    let currentDocumentData = null;
    let currentKycData = null;

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

            const previewButton = user.KycStatus === "SUBMITTED" && user.IsActive == true && user.IsBlocked == false ?
                `<button class="btn btn-sm btn-outline-danger preview-kyc" data-user-id="${user.Id}" title="Preview KYC Document">
            <i class="fas fa-file-alt"></i>
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
                    ${previewButton}
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

            const mobilePreviewButton = isPendingKYC && user.IsActive == true && user.IsBlocked == false ?
                `<button class="btn btn-sm btn-outline-danger preview-kyc" data-user-id="${user.Id}" title="Preview KYC Document">
            <i class="fas fa-file-alt"></i>
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
                    ${mobilePreviewButton}
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

    

    function GetUserAccounts() {
        $.ajax({
            type: "GET",
            url: "/User/GetUserAccounts",
            cache: false,
            success: function (response) {
                self.usersList = response && response.data ? response.data : [];
                self.filteredUsersList = [...self.usersList];
                genarateDropdown("RoleId", self.dropDownRolesList, "Id", "Name");
                self.populateSummaryCards();
                self.populateUserAccountsGrid();
                self.initializeSearch();
                self.initializeEventHandlers();
                $(".se-pre-con").hide();

            }, error: function (error) {
                console.log(error);
            }
        });
    };
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
                GetUserAccounts();
                $(".se-pre-con").hide();
            },
            errorCallback: function (error) {
                console.log('Error creating user:', error);
                $(".se-pre-con").hide();
            }
        });
    };

    //KYC Handling
    $(document).on('click', '.assign-kyc', function () {
        $('#initKycModal').modal({ backdrop: 'static', keyboard: false });
        const userId = $(this).data('user-id');
        const user = self.usersList.find(u => u.Id === userId);
        self.CurrentSelectedUser = user;
        $("#initKycModal").modal("show");
    });

    $(document).on("click", ".btn-view-card-close", function () {
        self.CurrentSelectedUser = {};
        self.PreviewUserKycDocument = {};
        $("#prviewKycModal").modal("hide");
    });

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
                GetUserAccounts();
                $(".se-pre-con").hide();
            },
            errorCallback: function (error) {
                console.log('Error creating user:', error);
                $(".se-pre-con").hide();
            }
        });
    });

    // Preview KYC Document
    $(document).on('click', '.preview-kyc', function () {
        $('#prviewKycModal').modal({ backdrop: 'static', keyboard: false });
        const userId = $(this).data('user-id');
        const user = self.usersList.find(u => u.Id === userId);
        self.CurrentSelectedUser = user;
        // Fetch KYC data from server
        fetchKycData(userId);

       
    });

    // Fetch KYC data from server
    function fetchKycData(userId) {
        // Show loading state
        $('#documentPreview').html('<div class="placeholder-content"><div class="spinner-border text-primary" role="status"></div><p class="text-muted mt-2">Loading KYC data...</p></div>');

        // API call to get KYC data
        $.ajax({
            url: '/User/PreviewUserKycDocument',
            type: 'GET',
            data: { userId: userId },
            success: function (response) {
                if (response.data) {
                    currentKycData = response.data;
                    self.PreviewUserKycDocument = currentKycData;
                    populateKycDetails(currentKycData);
                    loadDocumentPreview(currentKycData);
                    $("#prviewKycModal").modal("show");
                } else {
                    $("#prviewKycModal").modal("hide");
                }
            },
            error: function (xhr, status, error) {
                alert('Error loading KYC data: ' + error);
            }
        });
    }

    // Populate KYC details from both tables
    function populateKycDetails(kycData) {
        // Data from UserKyc table
        $('#kycUserId').text(kycData.userKyc.UserId || '-');
        $('#kycLevel').text(kycData.userKyc.KycLevel || 'Not Specified');
        $('#kycSubmittedOn').text(kycData.userKyc.SubmittedOn ? formatDateTime(kycData.SubmittedOn) : '-');
        $('#kycVerifiedOn').text(kycData.userKyc.VerifiedOn ? formatDateTime(kycData.VerifiedOn) : '-');
        $('#kycVerifiedBy').text(kycData.userKyc.VerifiedBy || '-');

        // Data from UserKycDocument table
        $('#kycDocumentType').text(kycData.userKycDocument.DocumentType || '-');
        $('#kycDocumentNumber').text(kycData.userKycDocument.DocumentNumber || '-');
        $('#kycExpiryDate').text(kycData.userKycDocument.ExpiryDate ? formatDate(kycData.userKycDocument.ExpiryDate) : '-');

        // Status with appropriate badge color
        const status = kycData.userKycDocument.KycStatus || kycData.userKycDocument.VerificationStatus || 'Pending';
        $('#kycStatusBadge').text(status).removeClass().addClass(getStatusBadgeClass(status));

        // Show rejection reason if exists
        if (kycData.userKyc.RejectionReason) {
            $('#rejectionReasonSection').show();
            $('#kycRejectionReason').text(kycData.userKyc.RejectionReason);
        } else {
            $('#rejectionReasonSection').hide();
        }
    }

    // Get badge class based on status
    function getStatusBadgeClass(status) {
        switch (status.toLowerCase()) {
            case 'verified':
            case 'approved':
                return 'badge bg-success';
            case 'rejected':
                return 'badge bg-danger';
            case 'pending':
            case 'submitted':
                return 'badge bg-warning';
            case 'inprogress':
                return 'badge bg-info';
            default:
                return 'badge bg-secondary';
        }
    }

    // Load document preview

    function displayDocumentPreview(base64String, fileExtension, container) {
        let html = '';

        if (['jpg', 'jpeg', 'png', 'gif', 'bmp'].includes(fileExtension)) {
            html = `<div class="image-preview-wrapper d-flex align-items-center justify-content-center h-100">
                    <img src="data:image/${fileExtension === 'jpg' ? 'jpeg' : fileExtension};base64,${base64String}"
                         alt="KYC Document" class="document-image img-fluid"
                         style="transform: scale(${currentZoom}); transition: transform 0.3s ease; max-width: 100%; max-height: 100%; object-fit: contain;">
                </div>`;
        } else if (fileExtension === 'pdf') {
            html = `<div class="pdf-preview-wrapper h-100">
                    <embed src="data:application/pdf;base64,${base64String}"
                         type="application/pdf" width="100%" height="100%"
                         style="border: 1px solid #dee2e6; border-radius: 4px;">
                </div>`;
        } else if (fileExtension === 'doc' || fileExtension === 'docx') {
            html = `<div class="d-flex align-items-center justify-content-center h-100">
                    <div class="alert alert-info text-center">
                        <i class="fas fa-file-word fa-3x text-primary mb-3"></i>
                        <h5>Word Document</h5>
                        <p>This is a Word document that cannot be previewed in browser.</p>
                        <button class="btn btn-primary mt-2" onclick="downloadDocument()">
                            <i class="fas fa-download me-2"></i>Download to View
                        </button>
                    </div>
                </div>`;
        } else {
            html = `<div class="d-flex align-items-center justify-content-center h-100">
                    <div class="alert alert-info text-center">
                        <i class="fas fa-file fa-3x text-secondary mb-3"></i>
                        <h5>Document Preview</h5>
                        <p>This document format (${fileExtension}) cannot be previewed in browser.</p>
                        <button class="btn btn-primary mt-2" onclick="downloadDocument()">
                            <i class="fas fa-download me-2"></i>Download to View
                        </button>
                    </div>
                </div>`;
        }

        container.html(html);
    }
    function loadDocumentPreview(kycData) {
        const previewContainer = $('#documentPreview');

        // Reset zoom
        currentZoom = 1;

        if (kycData.userKycDocument.DocumentFileData) {
            // Convert byte array to base64
            const base64String = arrayBufferToBase64(kycData.userKycDocument.DocumentFileData);

            // Determine file type from MIME type or data signature
            const fileExtension = detectFileTypeFromData(kycData.userKycDocument.DocumentFileData, base64String);

            setTimeout(() => {
                displayDocumentPreview(base64String, fileExtension, previewContainer);
            }, 500);

            currentDocumentData = {
                base64: base64String,
                fileName: 'kyc_document',
                fileType: fileExtension
            };
        } else {
            previewContainer.html('<div class="placeholder-content"><i class="fas fa-exclamation-triangle fa-2x text-warning mb-2"></i><p class="text-muted">No document available</p></div>');
        }
    }
    // Enhanced arrayBufferToBase64 function
    function arrayBufferToBase64(buffer) {
        if (typeof buffer === 'string') {
            // If it's already a base64 string, return as is
            return buffer;
        }

        if (buffer instanceof ArrayBuffer) {
            const bytes = new Uint8Array(buffer);
            let binary = '';
            for (let i = 0; i < bytes.byteLength; i++) {
                binary += String.fromCharCode(bytes[i]);
            }
            return btoa(binary);
        }

        // Handle byte array from C# (common scenario)
        if (Array.isArray(buffer)) {
            const bytes = new Uint8Array(buffer);
            let binary = '';
            for (let i = 0; i < bytes.length; i++) {
                binary += String.fromCharCode(bytes[i]);
            }
            return btoa(binary);
        }

        // If it's already base64, return as is
        return buffer;
    }

    // Detect file type from binary data
    function detectFileTypeFromData(buffer, base64String) {
        console.log('Buffer type:', typeof buffer, buffer);
        console.log('Base64 string length:', base64String ? base64String.length : 0);

        // If buffer is already a string, it's probably base64
        if (typeof buffer === 'string') {
            console.log('Buffer is a string, treating as base64');
            // Check for PDF signature in base64
            if (buffer.startsWith('JVBER') || atob(buffer.substring(0, 20)).includes('%PDF')) {
                return 'pdf';
            }
            return detectFromBase64(buffer);
        }

        // If it's an array, try to detect from bytes
        if (Array.isArray(buffer)) {
            console.log('Buffer is an array, length:', buffer.length);
            return detectFromByteArray(buffer);
        }

        // If it's ArrayBuffer
        if (buffer instanceof ArrayBuffer) {
            console.log('Buffer is ArrayBuffer');
            return detectFromArrayBuffer(buffer);
        }

        console.log('Unknown buffer type, using base64 detection');
        return detectFromBase64(base64String);
    }

    function detectFromBase64(base64String) {
        if (!base64String) return 'unknown';

        try {
            // PDF in base64 starts with JVBER (which is %PDF- encoded)
            if (base64String.startsWith('JVBER')) {
                return 'pdf';
            }

            // Decode a small portion to check the signature
            const decoded = atob(base64String.substring(0, 100));
            console.log('Decoded base64 prefix:', decoded.substring(0, 20));

            if (decoded.startsWith('%PDF')) {
                return 'pdf';
            }
            if (decoded.startsWith('\xFF\xD8\xFF')) {
                return 'jpg';
            }
            if (decoded.startsWith('\x89PNG')) {
                return 'png';
            }
            if (decoded.startsWith('GIF8')) {
                return 'gif';
            }
            if (decoded.startsWith('BM')) {
                return 'bmp';
            }
        } catch (error) {
            console.error('Error decoding base64:', error);
        }

        return 'unknown';
    }

    function detectFromByteArray(byteArray) {
        if (!byteArray || byteArray.length < 8) return 'unknown';

        // Convert first bytes to hex
        const hexString = byteArray.slice(0, 8)
            .map(b => b.toString(16).padStart(2, '0'))
            .join('')
            .toUpperCase();

        console.log('Byte array hex signature:', hexString);

        const signatures = {
            'FFD8FF': 'jpg',
            '89504E47': 'png',
            '47494638': 'gif',
            '424D': 'bmp',
            '25504446': 'pdf', // %PDF
            'D0CF11E0A1B11AE1': 'doc',
            '504B0304': 'docx'
        };

        for (const [signature, ext] of Object.entries(signatures)) {
            if (hexString.startsWith(signature)) {
                return ext;
            }
        }

        return 'unknown';
    }

    function detectFromArrayBuffer(arrayBuffer) {
        if (!arrayBuffer || arrayBuffer.byteLength < 8) return 'unknown';

        const bytes = new Uint8Array(arrayBuffer.slice(0, 8));
        const hexString = Array.from(bytes)
            .map(b => b.toString(16).padStart(2, '0'))
            .join('')
            .toUpperCase();

        console.log('ArrayBuffer hex signature:', hexString);

        const signatures = {
            'FFD8FF': 'jpg',
            '89504E47': 'png',
            '47494638': 'gif',
            '424D': 'bmp',
            '25504446': 'pdf',
            'D0CF11E0A1B11AE1': 'doc',
            '504B0304': 'docx'
        };

        for (const [signature, ext] of Object.entries(signatures)) {
            if (hexString.startsWith(signature)) {
                return ext;
            }
        }

        return 'unknown';
    }

    // Convert array buffer to base64
    function arrayBufferToBase64(buffer) {
        if (typeof buffer === 'string') {
            return buffer; // Already base64
        }

        if (buffer instanceof ArrayBuffer) {
            const bytes = new Uint8Array(buffer);
            let binary = '';
            for (let i = 0; i < bytes.byteLength; i++) {
                binary += String.fromCharCode(bytes[i]);
            }
            return btoa(binary);
        }

        // Handle byte array from C#
        if (Array.isArray(buffer)) {
            const bytes = new Uint8Array(buffer);
            let binary = '';
            for (let i = 0; i < bytes.length; i++) {
                binary += String.fromCharCode(bytes[i]);
            }
            return btoa(binary);
        }

        return buffer; // Return as is if already base64
    }
   
    // Format date time
    function formatDateTime(dateTimeString) {
        return new Date(dateTimeString).toLocaleString();
    }

    // Format date only
    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString();
    }

    // Zoom functionality
    $('#zoomInBtn').on('click', function () {
        if (currentZoom < 2) {
            currentZoom += 0.1;
            $('.document-image').css('transform', `scale(${currentZoom})`);
        }
    });

    $('#zoomOutBtn').on('click', function () {
        if (currentZoom > 0.5) {
            currentZoom -= 0.1;
            $('.document-image').css('transform', `scale(${currentZoom})`);
        }
    });

    // Download document
    $('#downloadDocumentBtn').on('click', function () {
        if (currentDocumentData) {
            const link = document.createElement('a');
            link.href = `data:application/octet-stream;base64,${currentDocumentData.base64}`;
            link.download = `kyc_document_${currentKycData.UserId}.${currentDocumentData.fileType}`;
            link.click();
        }
    });

    // Reject button handler
    $('#rejectKycBtn').on('click', function () {
        $('#rejectionMessageSection').show();
        $('#submitRejectionBtn').show();
        $('#rejectKycBtn').hide();
        $('#approveKycBtn').hide();
    });

    // Submit rejection
    $('#submitRejectionBtn').on('click', function () {
        const rejectionMessage = $('#rejectionMessage').val().trim();

        if (!rejectionMessage) {
            alert('Please provide a rejection reason.');
            return;
        }

        submitKycDecision('Rejected', rejectionMessage);
    });

    // Approve button handler
    $('#approveKycBtn').on('click', function () {
        console.log("you clicked aprove button");
        submitKycDecision('Verified');
    });

    // Submit KYC decision to server
    function submitKycDecision(status, rejectionReason = "") {
        $(".se-pre-con").show();
        console.log(JSON.stringify(self.PreviewUserKycDocument));

        const requestData = {
            UserKycId: self.PreviewUserKycDocument.userKyc.Id,
            UserKycDocumentId: self.PreviewUserKycDocument.userKycDocument.Id,
            Status: status,
            RejectedReason: rejectionReason,
            ModifiedBy: self.ApplicationUser.Id
        };

        $.ajax({
            url: '/User/VerifyUserKycDocument',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            success: function (response) {
                console.log(response.data);
                $('#rejectionMessage').val()
                $('#prviewKycModal').modal('hide');
                GetUserAccounts();
            },
            error: function (xhr, status, error) {
                alert('Error updating KYC status: ' + error);
            }
        });
    }

    // Reset modal when closed
    $('#prviewKycModal').on('hidden.bs.modal', function () {
        $('#rejectionMessageSection').hide();
        $('#submitRejectionBtn').hide();
        $('#rejectKycBtn').show();
        $('#approveKycBtn').show();
        $('#rejectionMessage').val('');
        currentZoom = 1;
        currentDocumentData = null;
        currentKycData = null;
    });
}