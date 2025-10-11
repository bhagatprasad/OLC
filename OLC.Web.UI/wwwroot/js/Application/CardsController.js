function CardsController() {
    var self = this;
    self.init = function (e) {
        $("#preloader").show();
        /*card number masking*/
        var cardNumberInputMask = new Inputmask("####-####-####-####");
        cardNumberInputMask.mask($('[id*=CardNumber]'));

        /*Expiry Masking*/
        var expiryMonthInputMask = new Inputmask("##/##");
        expiryMonthInputMask.mask($('[id*=ExpiryMonth]'));

        $(document).on("click", ".dataClass", function () {
            $('.modal').hide();
            $('.modal-backdrop').remove();
        });
        self.loadCards();
        self.loadCardActivity();
        $(document).on("click", "#btncreateCard", function (e) {
            self.AddCreditCard(e);
        });
        $(document).on("click", ".btnEditCreditCard", function (e) {
            var cardId = $(this).data("id");
            self.EditCreditCard(cardId);
        });
        $(document).on("click", ".transaction-item", function () {
            var activityId = $(this).data("id");
            self.loadUserCardsActivityDetails(activityId);
        });
    };

    self.loadCards = function () {
        $.ajax({
            url: "/Cards/LoadUserCards",
            type: 'GET',
            dataType: 'html', // added data type
            beforeSend: function () {
                $("#preloader").show();
            },
            complete: function () {
                $("#preloader").hide();
            },
            success: function (res) {
                $("#CardsPartial").html("");
                $("#CardsPartial").html(res);
                
            }
        });
    }
    self.loadCardActivity = function () {
        var take = $("#take").val();
        var skip = $("#skip").val();
        $.ajax({
            url: "/Cards/LoadUserCardsActivity",
            type: 'GET',
            data: { take: take, skip: skip },
            dataType: 'html', // added data type
            success: function (res) {
                $("#CardActivityDetailsListPartial").html("");
                $("#CardActivityDetailsListPartial").html(res);
            }
        });
    }
    self.loadUserCardsActivityDetails = function (id) {
        $.ajax({
            url: "/Cards/LoadUserCardsActivityDetails",
            type: 'GET',
            data: { id: id },
            dataType: 'html', // added data type
            success: function (res) {
                $("#CardActivityDetailsPartial").html("");
                $("#CardActivityDetailsPartial").html(res);
                $("#transaction-detail").modal('show');
            }
        });
    }
    self.EditedCreditCard = function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Cards/EditCreditCard",
            type: "POST",
            data: $('#updateCard').serialize(),
            dataType: 'json',
            success: function (responce) {
                $("#edit-card-details-data").modal('hide');
                self.loadCardActivity();
                self.loadCards();
            },
            error: function (err) {
                console.log(err);
            }
        }); // ajax call closing
    }
    self.EditCreditCard = function (id) {
        $.ajax({
            url: "/Cards/EditCreditCard",
            type: 'GET',
            data: { id: id },
            dataType: 'html', // added data type
            success: function (res) {
                $("#edit-card-details-data").modal('show');
                $("#EditCardPartial").html("");
                $("#EditCardPartial").html(res);
            }
        });
    }
    self.AddCreditCard = function (e) {
        e.preventDefault();
        $.ajax({
                url: "/Cards/CreateCard",
                type: "POST",
                data: $('#addCard').serialize(),
                dataType: 'json',
                beforeSend: function () {
                    $("#preloader").show();
                },
                complete: function () {
                    $("#preloader").hide();
                },
                success: function (responce) {
                    $("#add-new-card-details").modal('hide');
                    $('.modal-backdrop').remove();
                    self.loadCardActivity();
                    self.loadCards();
                },
                error: function (err) {
                    console.log(err);
                }
            }); // ajax call closing
        }
}