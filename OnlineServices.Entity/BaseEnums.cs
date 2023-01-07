namespace OnlineServices.Entity
{
    public enum BaseKindEnum
    {
        BaseInformation = 3, // اطلاعات پایه
        TechnicalSpecificationItems = 31, // آیتم های مشخصات فنی
        TypesOfCarWashServices = 32, // انواع خدمات کارواش
        TypesOfRepairServices = 33, // انواع خدمات تعمیرگاه
        TypesOfCarServiceCenters = 34, // انواع خدمات مراکز خدمات خودرو
        TypesOfAccessoryCenters = 35, // انواع خدمات یدکی

        TypesOfCarWashServicesWithOutWater = 321, //  خدمات کارواش بدون آب
        TypesOfCarWashServicesWithWater = 322 // خدمات کارواش با آب
    }


    public enum ServiceTypeEnum
    {
        TowTruckSeervice = 1, // درخواست سرویس خودروبر
        LicensePlateReplacementService = 2, // درخواست امور شماره گذاری و تعویض پلاک
        InsuranceService = 3, // درخواست امور بیمه ای و تامین خسارات ناشی از تصادفات
        ExpertingCarService = 4, // درخواست کارشناسی خودرو
    }


    public enum PersonTypeEnum
    {
        Management = 1, // مدیریت
        Operator = 2, // اپراتور
        Expert = 3, // کارشناس
        Employee = 4, // کارمند
        NormalUser = 5, // کاربر
        CommercialUser = 6 // کاربر تجاری
    }

    public enum StatusEnum
    {
        RequestServiceByUser = 1, // درخواست خدمت توسط کاربر
        WaitingAcceptanceByEmployee = 2, // ارجاع درخواست به کارمند-کارشناس
        AcceptanceRequestByEmployee = 3, // پذیرش درخواست توسط کارمند-کارشناس
        CancelRequestByUser = 4, // لغو درخواست توسط کاربر
        CancelRequestByEmployee = 5, // لغو درخواست توسط کارمند - کارشناس
        MovingToSpecifiedPosition = 6, // در حال حرکت به سوی موقعیت مشخص شده
        OfferingService = 7, // در حال ارائه خدمت
        EndOfServiceAndDeliveryToUser = 8, // پایان خدمت و تحویل به کاربر
        EndOfCarOpration = 9 ,// پایان عملیات روی خودرو
        MovingBackToCustomer = 10 //بازگشت به سوی مشتری
    }


    public enum CarTypeEnum
    {
        SUV = 30, // SUV
        Sedan = 31, // سدان
    }

    public enum FileTypeEnum
    {
        Picture = 37, // تصویر
        Video = 38, // ویدیو
        Voice = 39, // فایل صوتی
    }

    public enum SystemSettingEnum
    {
        StaticPrice = 2, // مبلغ ثابت ورودیه
        AverageSpeed = 3, // سرعت میانگین بر حسب متر بر دقیقه
        TimeRatio = 4, // ضریب قیمت طول زمان سفر
    }
}
