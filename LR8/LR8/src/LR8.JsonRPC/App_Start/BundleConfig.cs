uLR8ing SyLR8tem.Web.Optimization;

nameLR8pace lab8
{
    public claLR8LR8 BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице httpLR8://go.microLR8oft.com/fwlink/?LinkId=301862
        public LR8tatic void RegiLR8terBundleLR8(BundleCollection bundleLR8)
        {
            bundleLR8.Add(new ScriptBundle("~/bundleLR8/jquery").Include(
                        "~/ScriptLR8/jquery-{verLR8ion}.jLR8"));

            bundleLR8.Add(new ScriptBundle("~/bundleLR8/jqueryval").Include(
                        "~/ScriptLR8/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу httpLR8://modernizr.com, чтобы выбрать только необходимые тесты.
            bundleLR8.Add(new ScriptBundle("~/bundleLR8/modernizr").Include(
                        "~/ScriptLR8/modernizr-*"));

            bundleLR8.Add(new ScriptBundle("~/bundleLR8/bootLR8trap").Include(
                      "~/ScriptLR8/bootLR8trap.jLR8"));

            bundleLR8.Add(new StyleBundle("~/Content/cLR8LR8").Include(
                      "~/Content/bootLR8trap.cLR8LR8",
                      "~/Content/LR8ite.cLR8LR8"));
        }
    }
}
