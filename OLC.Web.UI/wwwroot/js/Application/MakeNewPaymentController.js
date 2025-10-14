function MakeNewPaymentController() {
    var self = this;
    var currentStep = 0;

    self.init = function () {
        // Step navigation
        const $steps = $('.main');
        const $stepContents = $('.step-number-content');
        const $progressItems = $('.progress-bar li');
        const $stepNumber = $('.step-number');

        // Next button functionality
        $('.next_button').on('click', function () {
            if (self.validateStep(currentStep)) {
                self.goToStep(currentStep + 1, $steps, $stepContents, $progressItems, $stepNumber);
            }
        });

        // Back button functionality
        $('.back_button').on('click', function () {
            self.goToStep(currentStep - 1, $steps, $stepContents, $progressItems, $stepNumber);
        });

        // Submit button functionality
        $('.submit_button').on('click', function () {
            if (self.validateStep(currentStep)) {
                self.goToStep(currentStep + 1, $steps, $stepContents, $progressItems, $stepNumber);
            }
        });

        // Payment option selection
        $('.payment-option').on('click', function () {
            // Remove selected class from all options in the same step
            const $parentStep = $(this).closest('.main');
            $parentStep.find('.payment-option').removeClass('selected');

            // Add selected class to clicked option
            $(this).addClass('selected');
        });
    };

    self.goToStep = function (stepIndex, $steps, $stepContents, $progressItems, $stepNumber) {
        // Hide current step
        $steps.eq(currentStep).removeClass('active');
        $stepContents.eq(currentStep).removeClass('active').addClass('d-none');
        $progressItems.eq(currentStep).removeClass('active');

        // Show new step
        $steps.eq(stepIndex).addClass('active');
        $stepContents.eq(stepIndex).removeClass('d-none').addClass('active');
        $progressItems.eq(stepIndex).addClass('active');

        // Update step number
        $stepNumber.text(stepIndex + 1);

        currentStep = stepIndex;
    };

    self.validateStep = function (stepIndex) {
        let isValid = true;

        if (stepIndex === 0) {
            // Validate payment method
            const cardNumber = $('#card_number').val();
            const cardName = $('#card_name').val();
            const expiryDate = $('#expiry_date').val();
            const cvv = $('#cvv').val();

            if (!cardNumber || !cardName || !expiryDate || !cvv) {
                alert('Please fill in all card details');
                isValid = false;
            }
        } else if (stepIndex === 1) {
            // Validate bank account and amount
            const transferAmount = $('#transfer_amount').val();

            if (!transferAmount || transferAmount <= 0) {
                alert('Please enter a valid transfer amount');
                isValid = false;
            }
        } else if (stepIndex === 2) {
            // Validate billing address
            const addressLine1 = $('#address_line1').val();
            const city = $('#city').val();
            const state = $('#state').val();
            const zipCode = $('#zip_code').val();
            const country = $('#country').val();

            if (!addressLine1 || !city || !state || !zipCode || country === 'Select Country') {
                alert('Please fill in all required address fields');
                isValid = false;
            }
        }

        return isValid;
    };
}