function UserBillingAddressController() {

    var self = this;

    self.ApplicationUser = {};

    self.UserBillingAddresses = [];

    self.Countries = [];

    self.States = [];

    self.Cities = [];

    self.CurrentSelectedBillingAddress = null;

    self.currentSelectedBillingAddress = {};

    self.init = function () {

        var appUserInfo = storageService.get('ApplicationUser');

        console.log(appUserInfo);

        if (appUserInfo) {

            self.ApplicationUser = appUserInfo;
        }

        GetUserBillingAddresses();
        GetCountries();
        GetStates();
        GetCities();

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

        function GetCountries() {
            $.ajax({
                type: "GET",
                url: "/Country/GetCountriesList",
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.Countries = response && response.data ? response.data : [];
                    genarateDropdown("Country", self.Countries, "Id", "Name");

                }, error: function (error) {
                    console.log(error);
                }
            });
        };

        function GetStates() {
            $.ajax({
                type: "GET",
                url: "/State/GetStatesList",
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.States = response ? response : [];
                    genarateDropdown("State", self.States, "Id", "Name");

                }, error: function (error) {
                    console.log(error);
                }
            });
        };

        function GetCities() {
            var cities = [
                { Id: 1, CountryId: 1, StateId: 11, Name: "Bangalore", Code: "BLR" },
                { Id: 2, CountryId: 1, StateId: 11, Name: "Mysore", Code: "MYS" },
                { Id: 3, CountryId: 1, StateId: 11, Name: "Mangalore", Code: "MNG" },
                { Id: 4, CountryId: 1, StateId: 14, Name: "Mumbai", Code: "MUM" },
                { Id: 5, CountryId: 1, StateId: 14, Name: "Pune", Code: "PUN" },
                { Id: 6, CountryId: 1, StateId: 14, Name: "Nagpur", Code: "NGP" },
                { Id: 7, CountryId: 1, StateId: 23, Name: "Chennai", Code: "CHE" },
                { Id: 8, CountryId: 1, StateId: 23, Name: "Coimbatore", Code: "CBE" },
                { Id: 9, CountryId: 1, StateId: 23, Name: "Madurai", Code: "MDU" },
                { Id: 10, CountryId: 2, StateId: 41, Name: "Los Angeles", Code: "LAX" },
                { Id: 11, CountryId: 2, StateId: 41, Name: "San Francisco", Code: "SFO" },
                { Id: 12, CountryId: 2, StateId: 41, Name: "San Diego", Code: "SAN" },
                { Id: 13, CountryId: 2, StateId: 68, Name: "New York City", Code: "NYC" },
                { Id: 14, CountryId: 2, StateId: 68, Name: "Buffalo", Code: "BUF" },
                { Id: 15, CountryId: 2, StateId: 68, Name: "Albany", Code: "ALB" },
                { Id: 16, CountryId: 2, StateId: 79, Name: "Houston", Code: "HOU" },
                { Id: 17, CountryId: 2, StateId: 79, Name: "Dallas", Code: "DAL" },
                { Id: 18, CountryId: 2, StateId: 79, Name: "Austin", Code: "AUS" },
                { Id: 19, CountryId: 3, StateId: 94, Name: "Edinburgh", Code: "EDI" }
            ];

            self.Cities = cities;

            genarateDropdown("City", self.Cities, "Id", "Name");

            //$.ajax({
            //    type: "GET",
            //    url: "/City/GetCities",
            //    cache: false,
            //    success: function (response) {
            //        console.log(response)


            //    }, error: function (error) {
            //        console.log(error);
            //    }
            //});
        };

        function getStatusBadge(address) {
            if (address.IsActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return `<span data-address-id="${address.Id}" class="badge bg-warning status-badge activiate-address">In Active</span>`;
            }
        }

        function loadUserBillingAddresses() {
            const tbody = $('#userBillingAddressesBody');
            tbody.empty(); // Clear existing rows

            if (self.UserBillingAddresses.length > 0) {
                self.UserBillingAddresses.forEach(function (address) {

                    const statusBadge = getStatusBadge(address);

                    const actionButtons = `
                <button class="btn btn-sm btn-outline-primary view-address" data-address-id="${address.Id}" data-address='${JSON.stringify(address)}' title="view address">
                    <i class="fas fa-eye"></i>
                </button>
                <button class="btn btn-sm btn-outline-warning edit-address" data-address-id="${address.Id}" data-address='${JSON.stringify(address)}' title="edit address">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger delete-address" data-address-id="${address.Id}" data-address='${JSON.stringify(address)}' title="delete address">
                    <i class="fas fa-trash"></i>
                </button>
            `;

                    const row = `<tr class="transaction-item">
                <td>#${address.Id}</td>
                <td>${address.AddessLineOne || 'N/A'}</td>
                <td>${address.Location || 'N/A'}</td>
                <td>${address.CityName || 'N/A'}</td>
                <td>${address.StateName || 'N/A'}</td>
                <td>${address.CountryName || 'N/A'}</td>
                <td>${address.PinCode || 'N/A'}</td>
                <td>${statusBadge}</td>
                <td>
                   ${actionButtons}
                </td>
            </tr>`;

                    tbody.append(row);
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
    }
}
