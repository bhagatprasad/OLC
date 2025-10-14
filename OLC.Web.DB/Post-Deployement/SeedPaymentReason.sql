/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [dbo].[PaymentReason] AS target
USING (
    VALUES 
        ('House Renovation', 'Home improvement and renovation expenses'),
        ('Shopping', 'General retail and merchandise purchases'),
        ('Grocery', 'Food and household supplies'),
        ('Utility Bills', 'Electricity, water, gas payments'),
        ('Rent Payment', 'Monthly rental expenses'),
        ('Mortgage', 'Home loan repayment'),
        ('Car Maintenance', 'Vehicle service and repairs'),
        ('Medical Expenses', 'Healthcare and medical bills'),
        ('Education Fees', 'School and tuition payments'),
        ('Insurance Premium', 'Insurance policy payments'),
        ('Travel Expenses', 'Business and personal travel'),
        ('Dining Out', 'Restaurant and food delivery'),
        ('Entertainment', 'Movies, concerts, events'),
        ('Subscription Services', 'Streaming, software subscriptions'),
        ('Gym Membership', 'Fitness center fees'),
        ('Home Appliances', 'Purchase of household electronics'),
        ('Furniture', 'Home furniture and decor'),
        ('Clothing', 'Apparel and accessories'),
        ('Electronics', 'Phones, laptops, gadgets'),
        ('Vehicle Purchase', 'Car or vehicle acquisition'),
        ('Fuel', 'Gasoline and vehicle fuel'),
        ('Public Transport', 'Bus, train, metro fares'),
        ('Tax Payment', 'Income and property taxes'),
        ('Charity Donation', 'Charitable contributions'),
        ('Investment', 'Stock market and investments'),
        ('Savings Deposit', 'Bank savings allocation'),
        ('Loan Repayment', 'Personal loan settlement'),
        ('Credit Card Payment', 'Credit card bill settlement'),
        ('Wedding Expenses', 'Marriage ceremony costs'),
        ('Birthday Celebration', 'Birthday party expenses'),
        ('Vacation', 'Holiday and travel packages'),
        ('Home Repair', 'House maintenance and fixes'),
        ('Pet Care', 'Veterinary and pet supplies'),
        ('Child Care', 'Babysitting and child expenses'),
        ('Professional Services', 'Legal, accounting fees'),
        ('Business Expenses', 'Office and business costs'),
        ('Software Purchase', 'Computer software and apps'),
        ('Books & Education', 'Educational materials'),
        ('Hobby Supplies', 'Craft and hobby materials'),
        ('Sports Equipment', 'Fitness and sports gear'),
        ('Beauty & Personal Care', 'Salon and grooming'),
        ('Home Cleaning', 'Cleaning services'),
        ('Gardening', 'Landscaping and plants'),
        ('Mobile Recharge', 'Phone credit and plans'),
        ('Internet Bill', 'Internet service payment'),
        ('Cable TV', 'Television subscription'),
        ('Parking Fees', 'Vehicle parking charges'),
        ('Tolls', 'Road and highway tolls'),
        ('Car Insurance', 'Vehicle insurance premium'),
        ('Health Insurance', 'Medical insurance payment'),
        ('Life Insurance', 'Life coverage premium'),
        ('Retirement Fund', 'Pension and retirement savings'),
        ('Emergency Fund', 'Unexpected expense coverage'),
        ('Gifts', 'Present and gift purchases'),
        ('Party Supplies', 'Event and celebration materials'),
        ('Home Insurance', 'Property insurance premium'),
        ('Property Tax', 'Real estate tax payment'),
        ('Student Loan', 'Education loan repayment'),
        ('Professional Development', 'Courses and certifications'),
        ('Conference Fees', 'Business event participation')
) AS source ([Name], [Description])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Description], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Description], NULL, SYSDATETIMEOFFSET(), NULL, SYSDATETIMEOFFSET(), 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Description] = source.[Description],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;