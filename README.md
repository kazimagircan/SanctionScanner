# SanctionScanner
Sanction Scanner Vaka Uygulaması
Sanction Scanner Masraf Uygulaması
Sistem Gereksinimi
Uygulamanın kullanılacağı windows ortamında .Net Core  3.1 ve MSSQL kurulu olmalıdır. EF tool kurulu olmayan bilgisayarlarda EF tool kurulmalıdır.

Sistem Kurulumu
Mssql'de <SanctionScanner> isimli database oluşturulur.


Database'iniz oluşturuldu. Sırada Entity Framework Orm aracı kullanılarak Migration işlmelerimiz gerçekleştiriyoruz.
<SanctionScanner.DAL > proje dizininde powershell ile erişilir.
 

 aşagıdaki komutlar ile Code First database migration gerçeleştirilir.
⦁	dotnet ef migrations add mig_1

⦁	dotnet ef database update
 

Database işlemleri tamamlanmıştır.
Proje katmanında 
< SanctionScanner\SanctionScanner.PL\wwwroot\js > path'inden <site.js> dosyasında domain adresi kontrol edilir. Linux ortamında port bilginiz 5000 olarak atanacaktır. Windows ortamında projeyi ayağa kaldırdığınızda hostingin atadığı port bilgisi ile bu kısmı degiştirebilirsiniz. Default olarak bu bilgi "domain:'https://localhost:44381'" olarak atanmıştır.
 
 


Sistem Kullanımı
Projeyi ayağa kadırdığınızda default olarak aşaıdaki kullanıcılar oluşturulmuştur. Test için kullanabilirsiniz.
User: Kullanıcı: user@gmail.com Pass: 123
Manager: Kullanıcı: manager@gmail.com Pass: 123
Accountant: Kullanıcı: accountant@gmail.com Pass: 123


Structure

SanctionScanner.BAL : Business Application Layer
Application katmanında data access layer ile User arayüzü arasında süreçler işletilmiştir.
SanctionScanner.DAL : Data Access Layer
Data Access Layer Entity Framework kullanılarak Code First yöntemi ile database oluşturulmuştur.
SanctionScanner.Infrastructure: Infrastructure Layer ( Emailer, Logger )
Infrasructure layerda microservisler yapılanmdırılmıştır. Logger servisi TODO olarak bırakılmıştır. Serilog, Nlog yada Log4Net kullanılabilir.
SanctionScanner.PL: Presentation Layer ( User Interface)
Authentication sisteminde .Net Core Identity kullanılmamıştır. Spesific olarak Filter kodlanmıştır. HomeController classından kontrol edilebilir. Fronend kısmında jquery kullanılmıştır.

Uygulama Tasarımı

User - manager ilişkisi
Uygulamaya erişimi olan her kullanıcıya atanmış bir manager vardır. Kullanıcın rolü manager olsa dahi başka bir kullanıcı kendisine manager olarak atanabilmektedir. Bu sayede Role denetimi yükü azaltılmış her kullanıcı kolaylıkla başkası tarafından denetlenebilir hale getirilmiştir.
Masraf
Masraflar için aşama kodu oluşturulmuş, her aşama için Masrafın bir aşama takip kodu vardır. StepCode olarak adlandırığımız bu sistem ile bir masrafın ilgili süreci hakkında fikir edinip takip edebiliyoruz.
 


