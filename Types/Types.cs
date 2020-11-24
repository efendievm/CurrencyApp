namespace TypesLibrary // библиотека классов
{
    // класс, содержащий информацию о валюте
    public struct Currency
    {
        public string Id { get; set; }
        public string RusName { get; set; }
        public string EngName { get; set; }
        public int? NumCode { get; set; }
        public string CharCode { get; set; }
    }
}