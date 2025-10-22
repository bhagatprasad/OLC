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

MERGE INTO [dbo].[State] AS target
USING (
    SELECT c.Id AS CountryId, s.Name, s.Code
    FROM (
        VALUES 
            ('India', 'Andhra Pradesh', 'AP'),
            ('India', 'Arunachal Pradesh', 'AR'),
            ('India', 'Assam', 'AS'),
            ('India', 'Bihar', 'BR'),
            ('India', 'Chhattisgarh', 'CT'),
            ('India', 'Goa', 'GA'),
            ('India', 'Gujarat', 'GJ'),
            ('India', 'Haryana', 'HR'),
            ('India', 'Himachal Pradesh', 'HP'),
            ('India', 'Jharkhand', 'JH'),
            ('India', 'Karnataka', 'KA'),
            ('India', 'Kerala', 'KL'),
            ('India', 'Madhya Pradesh', 'MP'),
            ('India', 'Maharashtra', 'MH'),
            ('India', 'Manipur', 'MN'),
            ('India', 'Meghalaya', 'ML'),
            ('India', 'Mizoram', 'MZ'),
            ('India', 'Nagaland', 'NL'),
            ('India', 'Odisha', 'OR'),
            ('India', 'Punjab', 'PB'),
            ('India', 'Rajasthan', 'RJ'),
            ('India', 'Sikkim', 'SK'),
            ('India', 'Tamil Nadu', 'TN'),
            ('India', 'Telangana', 'TG'),
            ('India', 'Tripura', 'TR'),
            ('India', 'Uttar Pradesh', 'UP'),
            ('India', 'Uttarakhand', 'UT'),
            ('India', 'West Bengal', 'WB'),
            ('India', 'Andaman and Nicobar Islands', 'AN'),
            ('India', 'Chandigarh', 'CH'),
            ('India', 'Dadra and Nagar Haveli and Daman and Diu', 'DN'),
            ('India', 'Delhi', 'DL'),
            ('India', 'Jammu and Kashmir', 'JK'),
            ('India', 'Ladakh', 'LA'),
            ('India', 'Lakshadweep', 'LD'),
            ('India', 'Puducherry', 'PY'),
            ('United States', 'Alabama', 'AL'),
            ('United States', 'Alaska', 'AK'),
            ('United States', 'Arizona', 'AZ'),
            ('United States', 'Arkansas', 'AR'),
            ('United States', 'California', 'CA'),
            ('United States', 'Colorado', 'CO'),
            ('United States', 'Connecticut', 'CT'),
            ('United States', 'Delaware', 'DE'),
            ('United States', 'Florida', 'FL'),
            ('United States', 'Georgia', 'GA'),
            ('United States', 'Hawaii', 'HI'),
            ('United States', 'Idaho', 'ID'),
            ('United States', 'Illinois', 'IL'),
            ('United States', 'Indiana', 'IN'),
            ('United States', 'Iowa', 'IA'),
            ('United States', 'Kansas', 'KS'),
            ('United States', 'Kentucky', 'KY'),
            ('United States', 'Louisiana', 'LA'),
            ('United States', 'Maine', 'ME'),
            ('United States', 'Maryland', 'MD'),
            ('United States', 'Massachusetts', 'MA'),
            ('United States', 'Michigan', 'MI'),
            ('United States', 'Minnesota', 'MN'),
            ('United States', 'Mississippi', 'MS'),
            ('United States', 'Missouri', 'MO'),
            ('United States', 'Montana', 'MT'),
            ('United States', 'Nebraska', 'NE'),
            ('United States', 'Nevada', 'NV'),
            ('United States', 'New Hampshire', 'NH'),
            ('United States', 'New Jersey', 'NJ'),
            ('United States', 'New Mexico', 'NM'),
            ('United States', 'New York', 'NY'),
            ('United States', 'North Carolina', 'NC'),
            ('United States', 'North Dakota', 'ND'),
            ('United States', 'Ohio', 'OH'),
            ('United States', 'Oklahoma', 'OK'),
            ('United States', 'Oregon', 'OR'),
            ('United States', 'Pennsylvania', 'PA'),
            ('United States', 'Rhode Island', 'RI'),
            ('United States', 'South Carolina', 'SC'),
            ('United States', 'South Dakota', 'SD'),
            ('United States', 'Tennessee', 'TN'),
            ('United States', 'Texas', 'TX'),
            ('United States', 'Utah', 'UT'),
            ('United States', 'Vermont', 'VT'),
            ('United States', 'Virginia', 'VA'),
            ('United States', 'Washington', 'WA'),
            ('United States', 'West Virginia', 'WV'),
            ('United States', 'Wisconsin', 'WI'),
            ('United States', 'Wyoming', 'WY'),
             -- United Kingdom (Nations and some regions)
            ('United Kingdom', 'England', 'ENG'),
            ('United Kingdom', 'Scotland', 'SCO'),
            ('United Kingdom', 'Wales', 'WAL'),
            ('United Kingdom', 'Northern Ireland', 'NIR'),
            ('United Kingdom', 'London', 'LON'),
            -- Canada (Provinces and Territories)
            ('Canada', 'Alberta', 'AB'),
            ('Canada', 'British Columbia', 'BC'),
            ('Canada', 'Manitoba', 'MB'),
            ('Canada', 'New Brunswick', 'NB'),
            ('Canada', 'Newfoundland and Labrador', 'NL'),
            -- Australia (States and Territories)
            ('Australia', 'New South Wales', 'NSW'),
            ('Australia', 'Queensland', 'QLD'),
            ('Australia', 'South Australia', 'SA'),
            ('Australia', 'Tasmania', 'TAS'),
            ('Australia', 'Victoria', 'VIC'),
            -- Germany (Länder)
            ('Germany', 'Baden-Württemberg', 'BW'),
            ('Germany', 'Bavaria', 'BY'),
            ('Germany', 'Berlin', 'BE'),
            ('Germany', 'Brandenburg', 'BB'),
            ('Germany', 'Bremen', 'HB'),
            -- France (Regions)
            ('France', 'Île-de-France', 'IDF'),
            ('France', 'Auvergne-Rhône-Alpes', 'ARA'),
            ('France', 'Hauts-de-France', 'HDF'),
            ('France', 'Nouvelle-Aquitaine', 'NAQ'),
            ('France', 'Occitanie', 'OCC'),
            -- Japan (Prefectures)
            ('Japan', 'Hokkaido', 'HKD'),
            ('Japan', 'Tokyo', 'TKY'),
            ('Japan', 'Osaka', 'OSK'),
            ('Japan', 'Kyoto', 'KYT'),
            ('Japan', 'Kanagawa', 'KNG'),
            -- Singapore (Districts)
            ('Singapore', 'Central', 'CTR'),
            ('Singapore', 'East', 'EST'),
            ('Singapore', 'North', 'NTH'),
            ('Singapore', 'North-East', 'NE'),
            ('Singapore', 'West', 'WST'),
            -- United Arab Emirates (Emirates)
            ('United Arab Emirates', 'Abu Dhabi', 'AD'),
            ('United Arab Emirates', 'Dubai', 'DU'),
            ('United Arab Emirates', 'Sharjah', 'SHJ'),
            ('United Arab Emirates', 'Ajman', 'AJM'),
            ('United Arab Emirates', 'Fujairah', 'FUJ'),
            -- China (Provinces and Municipalities)
            ('China', 'Beijing', 'BJ'),
            ('China', 'Shanghai', 'SH'),
            ('China', 'Guangdong', 'GD'),
            ('China', 'Shandong', 'SD'),
            ('China', 'Henan', 'HN'),
            -- Brazil (States)
            ('Brazil', 'São Paulo', 'SP'),
            ('Brazil', 'Rio de Janeiro', 'RJ'),
            ('Brazil', 'Minas Gerais', 'MG'),
            ('Brazil', 'Bahia', 'BA'),
            ('Brazil', 'Paraná', 'PR'),
            -- South Africa (Provinces)
            ('South Africa', 'Gauteng', 'GP'),
            ('South Africa', 'KwaZulu-Natal', 'KZN'),
            ('South Africa', 'Western Cape', 'WC'),
            ('South Africa', 'Eastern Cape', 'EC'),
            ('South Africa', 'Limpopo', 'LP'),
            -- Malaysia (States)
            ('Malaysia', 'Selangor', 'SLG'),
            ('Malaysia', 'Johor', 'JHR'),
            ('Malaysia', 'Penang', 'PNG'),
            ('Malaysia', 'Kedah', 'KDH'),
            ('Malaysia', 'Perak', 'PRK'),
            -- Sri Lanka (Provinces)
            ('Sri Lanka', 'Western', 'WP'),
            ('Sri Lanka', 'Central', 'CP'),
            ('Sri Lanka', 'Southern', 'SP'),
            ('Sri Lanka', 'Northern', 'NP'),
            ('Sri Lanka', 'Eastern', 'EP'),
            -- Bangladesh (Divisions)
            ('Bangladesh', 'Dhaka', 'DHK'),
            ('Bangladesh', 'Chittagong', 'CTG'),
            ('Bangladesh', 'Khulna', 'KHN'),
            ('Bangladesh', 'Rajshahi', 'RJH'),
            ('Bangladesh', 'Sylhet', 'SYL'),
            -- Nepal (Provinces)
            ('Nepal', 'Province No. 1', 'P1'),
            ('Nepal', 'Province No. 2', 'P2'),
            ('Nepal', 'Bagmati', 'BAG'),
            ('Nepal', 'Gandaki', 'GAN'),
            ('Nepal', 'Lumbini', 'LUM'),
            -- Pakistan (Provinces and Territories)
            ('Pakistan', 'Punjab', 'PB'),
            ('Pakistan', 'Sindh', 'SD'),
            ('Pakistan', 'Khyber Pakhtunkhwa', 'KP'),
            ('Pakistan', 'Balochistan', 'BA'),
            ('Pakistan', 'Islamabad Capital Territory', 'ICT'),
            -- Russia (Federal Subjects)
            ('Russia', 'Moscow', 'MOW'),
            ('Russia', 'Saint Petersburg', 'SPE'),
            ('Russia', 'Moscow Oblast', 'MOS'),
            ('Russia', 'Krasnodar Krai', 'KDA'),
            ('Russia', 'Sverdlovsk Oblast', 'SVE'),
            -- Italy (Regions)
            ('Italy', 'Lombardy', 'LOM'),
            ('Italy', 'Lazio', 'LAZ'),
            ('Italy', 'Campania', 'CAM'),
            ('Italy', 'Sicily', 'SIC'),
            ('Italy', 'Veneto', 'VEN')
    ) AS s(CountryName, Name, Code)
    INNER JOIN [dbo].[Country] c ON c.Name = s.CountryName
) AS source
ON target.Name = source.Name AND target.CountryId = source.CountryId
WHEN NOT MATCHED THEN
    INSERT ([CountryId], [Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.CountryId, source.Name, source.Code, NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Code] = source.Code,
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;