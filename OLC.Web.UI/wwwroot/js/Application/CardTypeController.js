function CardTypeController() {

    var self = this;
    self.pageTitle = "Card types";
    self.formTitle = "";
    self.gridTitle = "All card types";
    self.cardTypes = [];
    self.init = function () {

        var form = $('#AddEditTypeForm');

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


        getTypes();

        //setup page title 
        $("#pageTitle").text(self.pageTitle);

        //setup grid title
        $("#gridTitle").text(self.gridTitle);


        $(document).on("click", "#addTypeBtn", function () {
            $('#sidebar').addClass('show');
            self.formTitle = "Add account type";
            $("#formTitle").text(self.formTitle);
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });


        $('#AddEditTypeForm').on('submit', function (e) {
            e.preventDefault();
            console.log();
            $('#AddEditTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        function getTypes() {
            makeAjaxRequest({
                url: API_URLS.GetCardTypesAsync,
                type: 'GET',
                successCallback: handleSuccess,
                errorCallback: handleError
            });

        }
        function handleSuccess(response) {

            console.info(response);

            self.cardTypes = response && response.data ? response.data : [];

            self.LoadTypesGrid();

            $(".se-pre-con").hide();
        }

        function handleError(xhr, status, error) {
            console.error('Error loading account type data:', error);
            $('#gridBody').html(`
                <tr>
                    <td colspan="8" class="text-center text-danger">
                        Error loading account type data. Please try again.
                    </td>
                </tr>
            `);
            $(".se-pre-con").hide();
        }

        self.LoadTypesGrid = function () {
            const tbody = $('#gridBody');
            tbody.empty(); // Clear existing rows

            if (self.cardTypes.length === 0) {
                tbody.append(`<tr>
                    <td colspan="8" class="text-center text-muted">No accounts types found</td>
                    </tr>`);
                return;
            }
            self.cardTypes.forEach(function (item) {
                const statusBadge = getStatusBadge(item.IsActive);
                const createdOn = formatDate(item.CreatedOn);
                const modifiedOn = formatDate(item.ModifiedOn);
                // Generate action buttons based on role
                const actionButtons = `
                <button class="btn btn-sm btn-outline-primary view-type" data-id="${item.Id}" data-type='${item}' title="view type">
                    <i class="fas fa-eye"></i>
                </button>
                <button class="btn btn-sm btn-outline-warning edit-type" data-id="${item.Id}" data-type='${item}' title="edit type">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger delete-type" data-id="${item.Id}" data-type='${item}' title="delete type">
                    <i class="fas fa-trash"></i>
                </button>
            `;
                const row = `
            <tr class="user-account-item" data-type='${item}' data-id='${item.Id}'>
                <td><a style="cursor:pointer;color:blue;" class="view-type" data-id="${item.Id}">#${item.Id}</a></td>
                <td>${item.Name}</td>
                <td>${item.Code}</td>
                <td>${createdOn || 'N/A'}</td>
                <td>${modifiedOn || 'N/A'}</td>
                <td>${statusBadge}</td>
                <td>
                    ${actionButtons}
                </td>
            </tr>
        `;
                tbody.append(row);
            });
        }
    }
}