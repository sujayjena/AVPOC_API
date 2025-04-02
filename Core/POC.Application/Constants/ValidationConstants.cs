namespace POC.Application.Constants
{
    public static class ValidationConstants
    {
        public const string NameRegExp = @"^[a-zA-Z\s]+$";

        public const string UserNameRequied_Msg = @"Employee Name is required";
        public const string UserNameRegExp_Msg = @"Employee Name is Invalid";
        public const string UserNameRegExp = @"^[a-zA-Z\s]+$";
        public const int UserName_MaxLength = 100;
        public const string UserName_MaxLength_Msg = "More than 100 characters are not allowed for Employee Name";

        public const string EmailIdRequied_Msg = @"Email Id is required";
        public const string EmailRegExp = "^([a-zA-Z0-9_-]([-\\.\\w]*[a-zA-Z0-9_-])*@([a-zA-Z0-9_-][-\\w]*[a-zA-Z0-9_-]\\.)+[a-zA-Z]{2,9})$";
        public const string EmailRegExp_Msg = "Email Id is invalid";
        public const int Email_MaxLength = 200;
        public const string Email_MaxLength_Msg = "More than 200 characters are not allowed for Email Id";

        public const string MobileNumberRequied_Msg = @"Mobile Number is required";
        public const string MobileNumberRegExp = @"^[0-9]+$";
        public const string MobileNumberRegExp_Msg = "Mobile Number value is invalid";
        public const int MobileNumber_MaxLength = 10;
        public const string MobileNumber_MaxLength_Msg = "More than 10 characters are not allowed for Mobile Number";

        public const string PhoneNumberRequied_Msg = @"Phone Number is required";
        public const string PhoneNumberRegExp = @"^[0-9]+$";
        public const string PhoneNumberRegExp_Msg = "Phone Number value is invalid";
        public const int PhoneNumber_MaxLength = 10;
        public const string PhoneNumber_MaxLength_Msg = "More than 10 characters are not allowed for Phone Number";
        public const string LandlineNumber_MaxLength_Msg = "More than 10 characters are not allowed for Landline Number";

        public const int MobileUniqueId_MaxLength = 250;
        public const string MobileUniqueId_MaxLength_Msg = "More than 250 characters are not allowed for Mobile Unique Id";

        public const string GSTNumberRequired_Msg = @"GST Number is required";
        public const string GSTNumberRegExp = @"^[0-9a-zA-Z]+$";
        public const string GSTNumberRegExp_Msg = "GST Number value is invalid";
        public const int GSTNumber_MaxLength = 15;
        public const string GST_MaxLength_Msg = "More than 15 characters are not allowed for GST Number";

        public const string PANNumberRequired_Msg = @"PAN Number is required";
        public const string PANNumberRegExp = @"^[0-9a-zA-Z]+$";
        public const string PANNumberRegExp_Msg = "PAN Number value is invalid";
        public const int PANNumber_MaxLength = 10;
        public const string PANNumber_MaxLength_Msg = "More than 10 characters are not allowed for PAN Number";

        public const string PincodeRequied_Msg = @"Pincode is required";
        public const int Pincode_MinLength = 4;
        public const int Pincode_MaxLength = 11;
        public const string PincodeExp = @"^[0-9-]+$";
        public const string Pincode_MinLength_Msg = "Pincode must be of at least 4 character long";
        public const string Pincode_MaxLength_Msg = "More than 11 characters are not allowed for Pincode";
        public const string Pincode_Validation_Msg = "Pincode is Invalid";

        public const string OTP_Required_Msg = @"OTP is required";
        public const string OTP_RegExp = @"^[0-9]+$";
        public const string OTP_RegExp_Msg = "OTP value is invalid";
        public const int OTP_MinLength = 4;
        public const int OTP_MaxLength = 4;
        public const string OTP_Range_Msg = "OTP must be of 4 characters long";

        public const int Name_MaxLength = 100;
    }
}
