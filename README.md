# TravelStaff | Seyahat

Yönetici ve personeller tarafından şirket adına düzenlenen seyahatlerin kaydının tutulduğu ve bunların yönetici tarafından onaylanıp reddedilebildiği, gerçek zamanlı mesajlaşılabilen **kurumsal bir seyahat yönetimi** web sitesidir.

## Kullanılan Teknolojiler ve Özellikler

- ASP.NET Core 6.0 MVC
- N-Tier Mimarisi
- Entity Framework Core
- Code-First
- Repository Design Pattern
- DTO Katmanı
- SignalR
- Microsoft Sql Server
- AJAX
- Fluent Validation
- AutoMapper
- Dependency Injection: Autofac
- Background Service
  
## Demo Videoları
- Yönetici ve personel panelleri üzerinden sitenin genel kullanımını incelemek için [tıklayınız.](https://github.com/user-attachments/assets/04aa0f43-b918-4508-9fb5-0f4ab7417ca0)
- URL üzerinden yapılan izinsiz sayfa erişimlerinin nasıl engellendiğini izlemek için [tıklayınız.](https://github.com/user-attachments/assets/ad70b618-f3fa-47b1-9e3e-cf3879e66112)
  
## Proje Detayları
Bu proje, yönetici ve personellerin iş seyahat akışlarını düzenleyen ve iletişim içinde olmalarını sağlayan bir projedir.

### Güvenlik ve Yetkilendirme
- Kullanıcılar **'Admin'** ve **'Staff'** olarak rollerine göre yetkilendirilir.
- ASP.NET Core **Identity** sayesinde şifreleme ve kullanıcı adı ile oturum açma sağlanır.
- Sayfa bazlı erişimlerde URL üzerinden **izinsiz erişimin** engellenmesi maksadıyla **filtrelemeler** kullanılır.

<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/6da149b0-765e-4814-8bbe-362c02321055" />

### Gerçek Zamanlı Mesajlaşma
- **SignalR** tabanlı arayüz ile yönetici ve personeller arasında **dinamik** ve **anlık** mesajlaşma olanağı sağlanır.

<img width="1916" height="880" alt="Image" src="https://github.com/user-attachments/assets/fe1f0c85-a38f-4c1b-9643-3687c76a31f8" />

### Yönetici ve Personel Alanı
- **Yönetici:** Seyahat oluşturma, onay süreçlerinin yönetimi ve personel yetkilendirme
- **Personel:** Seyahat onay takibi, seyahat detaylarını ve geçmişini görüntüleme imkanı

## Proje Görselleri
Projenin yönetici ve personel açısından deneyimi bu bölümde yer almaktadır.

## Kaydol ve Giriş Yap
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/8a9c1220-8c3a-410d-aaa3-7328514e8824" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/041a3a84-ca14-4722-b888-88f63520a0eb" />

## Ana Sayfa
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/530cf740-8dfe-410c-a3ad-a2eb47991e60" />

## Yönetici Paneli
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/6c60c66f-281c-4b9c-bd48-b46c9adee1d6" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/e1d33c96-9af7-4606-927b-ff1beb2de75e" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/2eeacd08-f1e2-4d0f-8ff4-48b5e1472020" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/8548c6b2-9114-4707-8976-dd1d4582a432" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/0a514121-ec65-4dec-a5bd-444cd1b112f6" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/18f04e9b-d6b4-4b5f-8d4e-a62968cda380" />

## Personel Arayüzü
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/9045a10a-ecd0-4a37-b116-841a30c4adbe" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/b0a0cee3-b992-4cc2-9c92-b8348faf47cb" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/05aad86a-7f12-4e0a-95b6-8b87dbe5266a" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/c5311af7-80c1-40c7-a671-d8ef96692ead" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/ce9cdd90-7987-46f5-b192-528679cd0c7a" />
<img width="1920" height="1020" alt="Image" src="https://github.com/user-attachments/assets/212e3764-32ad-417d-8ecc-aa1ca3089534" />

