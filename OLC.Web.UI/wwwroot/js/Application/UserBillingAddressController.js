function UserBillingAddressController() {

    var self = this;

    self.ApplicationUser = {};

    self.UserBillingAddresses = [];

    self.Countries = [];

    self.States = [];

    self.Cities = [];

    self.CurrentSelectedBillingAddress = null;

    var actions = [];

    var dataObjects = [];

    self.init = function () {

        $(".se-pre-con").show();

        var appUserInfo = storageService.get('ApplicationUser');

        console.log(appUserInfo);

        if (appUserInfo) {

            self.ApplicationUser = appUserInfo;
        }
        actions.push("/BillingAddress/GetUserBillingAddresses");

        actions.push("/Country/GetCountriesList");

        actions.push("/State/GetStatesList");

        actions.push("/City/GetCitiesList");

        dataObjects.push({ userId: self.ApplicationUser.Id });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 0) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log(responses);

            self.UserBillingAddresses = responses[0][0] && responses[0][0].data ? responses[0][0].data : [];
            self.Countries = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];
            self.States = responses[2][0] && responses[2][0].data ? responses[2][0].data : [];
            self.Cities = responses[3][0] && responses[3][0].data ? responses[3][0].data : [];

            loadUserBillingAddresses();
            genarateDropdown("Country", self.Countries, "Id", "Name");
            genarateDropdown("State", self.States, "Id", "Name");
            genarateDropdown("City", self.Cities, "Id", "Name");

            $(".se-pre-con").hide();
        }).fail(function () {
            console.log('One or more requests failed.');
            hideLoader();
        });

        function getStatusBadge(address) {
            if (address.IsActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return `<span data-address-id="${address.Id}" class="badge bg-warning status-badge activiate-address">In Active</span>`;
            }
        }
        function loadUserBillingAddresses() {
            const tbody = $('#userBillingAddressesBody');
            const cardsContainer = $('#mobileBillingAddressesCards');
            tbody.empty(); // Clear existing desktop table rows
            cardsContainer.empty(); // Clear existing mobile cards

            if (self.UserBillingAddresses.length > 0) {
                self.UserBillingAddresses.forEach(function (address, index) {
                    const statusBadge = getStatusBadge(address);
                    var country = null;
                    var state = null;
                    var city = null;

                    if (address.CountryId)
                        country = self.Countries.filter(x => x.Id == address.CountryId)[0];

                    if (address.StateId)
                        state = self.States.filter(x => x.Id == address.StateId)[0];

                    if (address.CityId)
                        city = self.Cities.filter(x => x.Id == address.CityId)[0];

                    const actionButtons = `
                                            <button class="btn btn-sm btn-outline-primary view-address me-1" data-address-id="${address.Id}" data-address='${JSON.stringify(address).replace(/'/g, "&apos;")}' title="view address">
                                                <i class="fas fa-eye"></i>
                                            </button>
                                            <button class="btn btn-sm btn-outline-warning edit-address me-1" data-address-id="${address.Id}" data-address='${JSON.stringify(address).replace(/'/g, "&apos;")}' title="edit address">
                                                <i class="fas fa-edit"></i>
                                            </button>
                                            <button class="btn btn-sm btn-outline-danger delete-address" data-address-id="${address.Id}" data-address='${JSON.stringify(address).replace(/'/g, "&apos;")}' title="delete address">
                                                <i class="fas fa-trash"></i>
                                            </button>
                                         `;

                    // Desktop table row
                    const row = `<tr class="transaction-item">
                                <td>#${address.Id}</td>
                                <td>${address.AddressLineOne || 'N/A'}</td>
                                <td>${address.Location || 'N/A'}</td>
                                <td>${city.Name || 'N/A'}</td>
                                <td>${state.Name || 'N/A'}</td>
                                <td>${country.Name || 'N/A'}</td>
                                <td>${address.PinCode || 'N/A'}</td>
                                <td>${statusBadge}</td>
                                <td>
                                   ${actionButtons}
                                </td>
                                 </tr>`;
                    tbody.append(row);

                    // Mobile card
                    const cardHtml = `
                                    <div class="card mb-3 pt-2">
                                        <div class="card-header">
                                            <strong>${address.AddressLineOne || 'N/A'} (${city.Name || 'N/A'})</strong>
                                        </div>
                                        <div class="card-body">
                                            <p class="card-text mb-1"><strong>ID:</strong> #${address.Id}</p>
                                            <p class="card-text mb-1"><strong>Location:</strong> ${address.Location || 'N/A'}</p>
                                            <p class="card-text mb-1"><strong>State:</strong> ${state.Name || 'N/A'}</p>
                                            <p class="card-text mb-1"><strong>Country:</strong> ${country.Name || 'N/A'}</p>
                                            <p class="card-text mb-1"><strong>Pin Code:</strong> ${address.PinCode || 'N/A'}</p>
                                            <p class="card-text mb-1"><strong>Status:</strong> ${statusBadge}</p>
                                        </div>
                                        <div class="card-footer d-flex justify-content-between">
                                            ${actionButtons}
                                        </div>
                                    </div>
                                `;
                    cardsContainer.append(cardHtml);
                });
            }
        }


        $(document).on("click", ".activiate-address", function () {
            var addressId = $(this).data("address-id");

            console.log(addressId);
        });

        $(document).on("click", "#addBillingAddressBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditUserBillingAddressForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-address", function () {
            var addressId = $(this).data("address-id");
            var selectedBillingAddress = self.UserBillingAddresses.filter(x => x.Id == addressId)[0];

            console.log("current selected user billing address is .." + JSON.stringify(selectedBillingAddress));

            self.CurrentSelectedBillingAddress = selectedBillingAddress;

            var country = null;
            var state = null;
            var city = null;

            if (self.CurrentSelectedBillingAddress.CountryId)
                country = self.Countries.filter(x => x.Id == self.CurrentSelectedBillingAddress.CountryId)[0];

            if (self.CurrentSelectedBillingAddress.StateId)
                state = self.States.filter(x => x.Id == self.CurrentSelectedBillingAddress.StateId)[0];

            if (self.CurrentSelectedBillingAddress.CityId)
                city = self.Cities.filter(x => x.Id == self.CurrentSelectedBillingAddress.CityId)[0];

            $("#AddressLineOne").val(self.CurrentSelectedBillingAddress.AddessLineOne);
            $("#AddressLineTwo").val(self.CurrentSelectedBillingAddress.AddessLineTwo);
            $("#AddressLineThree").val(self.CurrentSelectedBillingAddress.AddessLineThress);
            $("#Location").val(self.CurrentSelectedBillingAddress.Location);

            if (country)
                $("#Country").val(country.Id);

            if (state)
                $("#State").val(state.Id);

            if (city)
                $("#City").val(city.Id);

            $("#PinCode").val(self.CurrentSelectedBillingAddress.PinCode);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');

            console.log("Iam getting from edit button click");
        });

        $(document).on("click", ".btn-view-address-close", function () {
            self.CurrentSelectedBillingAddress = null;
            $("#viewBillingAddress").modal("hide");
            $("#deleteBillingAddress").modal("hide");
        });

        $(document).on("click", ".delete-address", function () {
            console.log("deleting...");

            var addressId = $(this).data("address-id");

            var selectedBillingAddress = self.UserBillingAddresses.filter(x => x.Id == addressId)[0];

            console.log("current selected user billing address is .." + JSON.stringify(selectedBillingAddress));

            self.CurrentSelectedBillingAddress = selectedBillingAddress;

            $("#DeleteAddressLineOne").val(self.CurrentSelectedBillingAddress.AddessLineOne);
            $("#DeleteAddressLineTwo").val(self.CurrentSelectedBillingAddress.AddessLineTwo);
            $("#DeleteAddressLineThree").val(self.CurrentSelectedBillingAddress.AddessLineThress);
            $("#DeleteLocation").val(self.CurrentSelectedBillingAddress.Location);
            $("#DeletePinCode").val(self.CurrentSelectedBillingAddress.PinCode);

            $("#deleteBillingAddress").modal("show");
        });

        $(document).on("click", "#deleteBillingAddressBtn", function () {
            console.log("delete yes clicked");
            $.ajax({
                type: "DELETE",
                url: "/BillingAddress/DeleteUserBillingAddress",
                data: { billingAddressId: self.CurrentSelectedBillingAddress.Id },
                cache: false,
                success: function (response) {

                    console.log(response);

                    self.CurrentSelectedBillingAddress = null;

                    $("#deleteBillingAddress").modal("hide");

                    GetUserBillingAddresses();

                }, error: function (error) {
                    console.log(error);
                }
            });

        });

        $(document).on("click", ".view-address", function () {

            console.log("Viewing address");

            var addressId = $(this).data("address-id");

            var selectedBillingAddress = self.UserBillingAddresses.filter(x => x.Id == addressId)[0];

            console.log("current selected user billing address is .." + JSON.stringify(selectedBillingAddress));

            self.CurrentSelectedBillingAddress = selectedBillingAddress;

            $("#ViewAddressLineOne").val(self.CurrentSelectedBillingAddress.AddessLineOne);
            $("#ViewAddressLineTwo").val(self.CurrentSelectedBillingAddress.AddessLineTwo);
            $("#ViewAddressLineThree").val(self.CurrentSelectedBillingAddress.AddessLineThress);
            $("#ViewLocation").val(self.CurrentSelectedBillingAddress.Location);
            $("#ViewPinCode").val(self.CurrentSelectedBillingAddress.PinCode);
            $("#viewBillingAddress").modal("show");

        });

        $('#AddEditUserBillingAddressForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            var addressLineOne = $("#AddressLineOne").val();
            var addressLineTwo = $("#AddressLineTwo").val();
            var addressLineThree = $("#AddressLineThree").val();
            var location = $("#Location").val();
            var countryId = $("#Country").val();
            var stateId = $("#State").val();
            var cityId = $("#City").val();
            var pinCode = $("#PinCode").val();

            console.log(addressLineOne);

            var userAddress = {
                Id: self.CurrentSelectedBillingAddress ? self.CurrentSelectedBillingAddress.Id : 0,
                UserId: self.ApplicationUser.Id,
                AddessLineOne: addressLineOne,
                AddessLineTwo: addressLineTwo,
                AddessLineThress: addressLineThree,
                Location: location,
                CountryId: countryId,
                StateId: stateId,
                CityId: cityId,
                PinCode: pinCode,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                IsActive: true
            };

            console.log("userAddress...." + JSON.stringify(userAddress));

            $.ajax({
                type: "POST",
                url: "/BillingAddress/SaveUserBillingAddress",
                cache: false,
                data: JSON.stringify(userAddress),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    console.log(response)
                    self.CurrentSelectedBillingAddress = null;
                    $('#AddEditUserBillingAddressForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetUserBillingAddresses();

                }, error: function (error) {
                    console.log(error);
                }
            });

        });

        // Event handler for Country dropdown change
        $('#Country').change(function () {
            var countryId = $(this).val();

            // Reload states based on selected country ID
            var filteredStates = self.States.filter(function (state) {
                return state.CountryId == countryId;
            });
            genarateDropdown("State", filteredStates, "Id", "Name");

            // Clear and reload cities based on selected country ID (before state selection)
            var filteredCities = self.Cities.filter(function (city) {
                return city.CountryId == countryId;
            });
            genarateDropdown("City", filteredCities, "Id", "Name");
        });

        // Event handler for State dropdown change
        $('#State').change(function () {
            var stateId = $(this).val();

            // Reload cities based on selected state ID (state-level cities)
            var filteredCities = self.Cities.filter(function (city) {
                return city.StateId == stateId;
            });
            genarateDropdown("City", filteredCities, "Id", "Name");
        });

        function GetUserBillingAddresses() {
            $.ajax({
                type: "GET",
                url: "/BillingAddress/GetUserBillingAddresses",
                data: { userId: self.ApplicationUser.Id },
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.UserBillingAddresses = response && response.data ? response.data : [];
                    loadUserBillingAddresses();
                }, error: function (error) {
                    console.log(error);
                }
            });
        };
    }
}
