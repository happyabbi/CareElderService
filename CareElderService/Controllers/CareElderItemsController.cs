using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileUploadsInAspNetMvc.DAL;
using FileUploadsInAspNetMvc.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using ReflectSoftware.Facebook.Messenger.Client;
using ReflectSoftware.Facebook.Messenger.Common.Models.Client;
using System.Threading.Tasks;
using FileUploadsInAspNetMvc.Helper;
using ReflectSoftware.Facebook.Messenger.Common.Models;
using System.IO;
using Newtonsoft.Json;

namespace FileUploadsInAspNetMvc.Controllers
{
    public class CareElderItemsController : Controller
    {
        private DatabaseContext db;
        private string FB_page_access_token = "EAAXPXZACvK5ABACKS2p0dMZAZAI1Pe1pFupNnhoN0U4ExeDJE7YLIILlhZBTeSFE0a0LosW2ZBLCZBiGWIuMkFRFol6K4UMWZAqq5X4fOU4OJB53VnoTjXT1X1eqRZA64bBEzhGZAEJykWzi9rphuLuOhyTCrzAeGYXEZAUHOP5TFV8gZDZD";
        private string webUrl = "https://fileuploadsinmvc.azurewebsites.net";
        private const string sType1 = "用藥提醒", sType2 = "用餐提醒", sType3 = "運動提醒";
        List<string> medicineEduList = new List<string>();
        List<string> exerciseEduList = new List<string>();
        List<string> videoEduList = new List<string>();

        List<string> videoEduList1 = new List<string>();
        List<string> videoEduList2 = new List<string>();


        public CareElderItemsController()
        {
            medicineEduList.Add("勿服不必要服的藥物。例如，濫用抗生素會使細菌迅速產生抗藥性，使得抗生素對已產生抗藥性的病原菌無效，須要付出很大的社會成本才能再發展出新一代的抗生素。");
            medicineEduList.Add("不可因醫師所開的藥覺得無效就隨意停藥。以抗黴藥物為例，治療香港腳至少須六週的時間才會痊癒，其間貿然停藥，病原菌開始大反撲，病沒有醫好還可能產生抗藥性。");
            medicineEduList.Add("不要因症狀加重就自行加倍服藥，以免發生藥物的不良反應和毒性產生。");
            medicineEduList.Add("不可因希望早日痊癒而把數種藥物一起服下。例如，一次服用數種相同療效的藥物；或是一天應分3次服用的藥物，一次就服用完畢。嚴重時可能造成藥物的不良反應或中毒而危及生命。");
            medicineEduList.Add("為了您的用藥安全，一般市面上的藥品是依照藥品的安全性高低分成三級即「處方藥、指示藥、成藥」，了解藥品分級制度讓我們在使用上更安心。");

            exerciseEduList.Add("運動會受飲食、睡眠、疾病、壓力...等生理及心理因素影響，請隨時注意自己的身體狀況，切勿逞強去做超出自己體能範圍的運動，以免造成傷害、過度疲勞或不適，甚而對運動喪失信心。");
            exerciseEduList.Add("運動中如有不適，如運動前或運動時出現疲憊、胸悶、盜汗、眩暈、噁心、呼吸困難或臉色發白等費力症狀時，表示心臟無法承受此活動量，就應立即停止並充分休息。");
            exerciseEduList.Add("不要在炎熱氣候、空氣污染嚴重和大太陽下運動。適合在清晨、下午稍晚或在溫度適中的冷氣房運動。");
            exerciseEduList.Add("運動前的暖身運動，每次運動前，都應先做 5 分鐘的熱身運動（如擺動手臂步行），及 5 分鐘的伸展運動（如：屈體側彎、屈體下壓），都是可讓你免除運動傷害的小撇步。 ");
            exerciseEduList.Add("由測量運動時的脈搏數，可得知運動強度，若以運動時的脈膊數為調整指標，比較容易控制適當的運動強度。請學會自我測量脈搏的方法，俾便調整自己的運動強度。");

            videoEduList.Add("//www.youtube.com/embed/N_66uq2taY8");
            videoEduList.Add("//www.youtube.com/embed/UIXePhrkiiU");
            videoEduList.Add("//www.youtube.com/embed/jwuF_OHSaww");
            videoEduList.Add("//www.youtube.com/embed/aFPbt2bIbmw");
            videoEduList.Add("//www.youtube.com/embed/7jQHv2iyOPs");

            videoEduList1.Add("//www.youtube.com/embed/0KCog3g37NA");
            videoEduList1.Add("//www.youtube.com/embed/XL_PxSxmKh0");
            videoEduList1.Add("//www.youtube.com/embed/V7AT7Me8DUg");
            videoEduList1.Add("//www.youtube.com/embed/xIXuLvjGpJ8");
            videoEduList1.Add("//www.youtube.com/embed/S-fNXmh99gk");


            


            this.db = new DatabaseContext();
            _clientMessenger = new MyClientMessenger(FB_page_access_token);

        }

        public CareElderItemsController(DatabaseContext dbContext)
        {
            this.db = dbContext;
        }

        // GET: CareElderItems
        public ActionResult Index(string message,string PSId)
        {
            ViewBag.Message = message;
            ViewBag.PSId = PSId;
            return View(db.CareElderItems.ToList());
        }

        // GET: CareElderItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareElderItem careElderItem = db.CareElderItems.Find(id);
            if (careElderItem == null)
            {
                return HttpNotFound();
            }
            return View(careElderItem);
        }



        // POST: CareElderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PSId,Sender,CareSubject,SubjectType,CareContent,ContentType,Memo")] CareElderItem careElderItem)
        {
            if (ModelState.IsValid)
            {
                db.CareElderItems.Add(careElderItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(careElderItem);
        }
        */



        // GET: CareElderItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareElderItem careElderItem = db.CareElderItems.Find(id);
            if (careElderItem == null)
            {
                return HttpNotFound();
            }
            return View(careElderItem);
        }

        // POST: CareElderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PSId,Sender,CareSubject,SubjectType,CareContent,ContentType,Memo")] CareElderItem careElderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(careElderItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(careElderItem);
        }

        // GET: CareElderItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareElderItem careElderItem = db.CareElderItems.Find(id);
            if (careElderItem == null)
            {
                return HttpNotFound();
            }
            return View(careElderItem);
        }

        // POST: CareElderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CareElderItem careElderItem = db.CareElderItems.Find(id);
            db.CareElderItems.Remove(careElderItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private readonly MyClientMessenger _clientMessenger;

        public async Task<ActionResult> Send(int? id, string preSend)
        {
                        
            if (id == null)
            {
                return RedirectToAction("Index", "CareElderItems", new { message = "noData"});
            }
            CareElderItem careElderItem = db.CareElderItems.Find(id);

            string PSId = careElderItem.PSId;
            string subjectType = careElderItem.SubjectType.Trim();
            string subject = careElderItem.CareSubject;
            string contentType = careElderItem.ContentType;//高血脂 
            string content = careElderItem.CareContent; //藥品  Lovastatin
            string imageUrl = careElderItem.ImageUrl;

            // CareElderItems 中沒有圖片時，就從 CareElderImages 中隨機挑一張
            if (imageUrl != null) {
                imageUrl = webUrl + imageUrl.Replace("\\", "/").Replace("~", "");
            }
            else
            {
                var imageArray = db.CareElderImages.ToArray();
                Random r = new Random();
                int randomIndex = r.Next(0, imageArray.Length - 1);
                imageUrl = webUrl + imageArray[randomIndex].ImageUrl;
                // 如果 Array 是 null 要給 default Image

            }

            List<Button> myButtonList = new List<Button>();

            DateTime saveNow = DateTime.UtcNow;

            DateTime utcDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);

            TimeZoneInfo TW_TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");

            DateTime localVersion = TimeZoneInfo.ConvertTime(utcDt, TW_TimeZoneInfo);

            //DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now, timeInfo);

            if (preSend == "true")
            {
                //todo
                string note = "藥物可能與食物(含酒精、牛奶)產生交互作用，請注意藥物包裝之說明。";
                string sendMessage = "";

                Random r = new Random();
                int randomIndex = 0;
                MessageResult messageResult;
                switch (subjectType)
                {

                    case sType1:
                        //用藥衛教 文字
                        randomIndex = r.Next(0, medicineEduList.Count - 1);
                        note = medicineEduList.ToArray()[randomIndex];

                        sendMessage = "用藥常識：" + note + localVersion.ToString("yyyy/MM/dd HH:mm");
                        messageResult = await _clientMessenger.SendMessageAsync(PSId, new TextMessage
                        {
                            Text = sendMessage
                        });

                        break;

                    case sType2:
                        //在用餐提醒前 先告知藥物是否和食品產生交互作用，所以要先取得 "用藥提醒 sType1"的 careContent
                        var tempCareElderItem = db.CareElderItems.Where(c => c.PSId == PSId && c.SubjectType == sType1);
                        string careSubject = "";
                        string[] medicines;
                        if (tempCareElderItem.Count() > 0)
                        {
                            medicines = tempCareElderItem.First().CareContent.Split(',');
                            careSubject = tempCareElderItem.First().CareSubject;
                        }
                        else {
                            medicines = new string[]{""};
                        }
                        foreach (var item in medicines)
                        {
                            var careElderMedicines = db.CareElderMedicines2.Where(c => c.EName.StartsWith(item));
                            if (careElderMedicines.Count() > 0)
                            {
                                string pc = careElderMedicines.First().PackageCode;
                                if (!string.IsNullOrEmpty(pc))
                                    note = pc;
                            }

                            sendMessage = "等一下吃完飯要吃" + careSubject + "藥囉，" + item + "藥品注意事項：" + note + localVersion.ToString("yyyy/MM/dd HH:mm");
                            messageResult = await _clientMessenger.SendMessageAsync(PSId, new TextMessage
                            {
                                Text = sendMessage
                            });
                        }


                        break;

                    case sType3: //高血脂衛教影片

                        randomIndex = r.Next(0, exerciseEduList.Count - 1);
                        note = exerciseEduList.ToArray()[randomIndex];

                        sendMessage = "運動常識：" + note + localVersion.ToString("yyyy/MM/dd HH:mm");
                        messageResult = await _clientMessenger.SendMessageAsync(PSId, new TextMessage
                        {
                            Text = sendMessage
                        });
                        break;

                }

            }
            else
            {
                switch (subjectType)
                {
                    case sType1://用藥提醒
                        myButtonList.Add(new MyButton("web_url")
                        {
                            Url = "https://fileuploadsinmvc.azurewebsites.net/CareElderItems/CreateMedicineRecord?PSId=" + PSId + "&Subject=" + subject,
                            Title = "有服藥"
                        });
                        myButtonList.Add(new MyButton("web_url")
                        {
                            Url = "https://fileuploadsinmvc.azurewebsites.net/CareElderItems/CreateMedicineRecord?PSId=" + PSId + "&Subject=" + subject,
                            Title = "未服藥"
                        });

                        content = "現在時間 " + localVersion.ToString("HH:mm") + "，請服用 " + content;
                        break;
                    case sType2://用餐提醒
                        myButtonList.Add(new MyButton("web_url")
                        {
                            Url = "https://fileuploadsinmvc.azurewebsites.net/CareElderItems/CreateEatingRecord?PSId=" + PSId,
                            Title = "有用餐"
                        });
                        myButtonList.Add(new MyButton("web_url")
                        {
                            Url = "https://fileuploadsinmvc.azurewebsites.net/CareElderItems/CreateEatingRecord?PSId=" + PSId,
                            Title = "未用餐"
                        });
                        content = "現在時間 " + localVersion.ToString("HH:mm") + "，" + content;
                        break;
                    case sType3://運動提醒
                        myButtonList.Add(new MyButton("web_url")
                        {
                            Url = "https://fileuploadsinmvc.azurewebsites.net/CareElderItems/CreateExercisingRecord?PSId=" + PSId,
                            Title = "有運動"
                        });
                        myButtonList.Add(new MyButton("web_url")
                        {
                            Url = "https://fileuploadsinmvc.azurewebsites.net/CareElderItems/CreateExercisingRecord?PSId=" + PSId,
                            Title = "未運動"
                        });
                        content = "現在時間 " + localVersion.ToString("HH:mm") + "，" + content;
                        break;

                }

                List<GenericElement> genericElementList = new List<GenericElement>();

                genericElementList.Add(new GenericElement
                {
                    Title = subjectType + "：" + subject,
                    ImageUrl = imageUrl,
                    Subtitle = content,
                    Buttons = myButtonList
                });



                var attachment = new GenericTemplateAttachment(genericElementList);

                //var a2 = new ListTemplateAttachment(listElementList, "compact", null);
                
                var result = await _clientMessenger.SendTemplateAttachmentAsync(PSId, attachment);

                //var json = JsonConvert.SerializeObject(new AttachmentMessage(a2));

                //ViewBag.Json = json;

            }

            //var attachment = new ButtonTemplateAttachment(subjectType, myButtonList);



            return RedirectToAction("Index", "CareElderItems", new { message = "OK" });
        }


        // GET: CareElderItems/Create
        public ActionResult CreateMedicine(string PSId)
        {
            ViewBag.PSId = PSId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMedicine(CareElderItemViewModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            /*
                        if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
                        {
                            ModelState.AddModelError("ImageUpload", "This field is required");
                        }
                        else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
                        {
                            ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                        } 
                         */


            if (ModelState.IsValid)
            {

                var careElderItem = new CareElderItem
                {
                    PSId = model.PSId,
                    Sender = model.Sender,
                    SubjectType = model.SubjectType,
                    CareSubject = model.CareSubject,
                    ContentType = model.ContentType,
                    CareContent = model.CareContent,
                    Memo = model.Memo,
                    SendTime1 = model.SendTime1,
                    SendTime2 = model.SendTime2,
                    SendTime3 = model.SendTime3,
                    SendTime4 = model.SendTime4

                };

                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                    careElderItem.ImageUrl = imageUrl;
                }


                db.CareElderItems.Add(careElderItem);
                db.SaveChanges();
                return RedirectToAction("Index", "CareElderItems", new { SubjectType = careElderItem.SubjectType, PSId = model.PSId });
            }

            return View(model);
        }

        public ActionResult MultipleUpload(string PSId)
        {
            ViewBag.PSId = PSId;
            return View(db.CareElderImages.ToList());
        }

        [HttpPost]
        public ActionResult MultipleUpload(IEnumerable<HttpPostedFileBase> files, string thePSId, string subjectType)
        {
            List<string> fileList = new List<string>();
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fileExt = Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("/uploads"), fileName + fileExt );
                    string imageUrl = "/uploads/" + fileName + fileExt;
                    file.SaveAs(filePath);
                    fileList.Add(imageUrl);

                    CareElderImage careElderImage = new CareElderImage {
                        PSId = thePSId,
                        SubjectType = subjectType,
                        ImageUrl = imageUrl
                    };

                    db.CareElderImages.Add(careElderImage);


                }
            }
            db.SaveChanges();
            ViewBag.FileList = fileList;
            return View(db.CareElderImages.ToList());
        }

        // GET: CareElderItems/Create
        public ActionResult CreateEating(string PSId)
        {
            ViewBag.PSId = PSId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEating(CareElderEatingViewModel model)
        {
            if (ModelState.IsValid)
            {

                var careElderItem = new CareElderItem
                {
                    PSId = model.PSId,
                    Sender = model.Sender,
                    SubjectType = model.SubjectType,
                    CareSubject = model.CareSubject,
                    ContentType = model.ContentType,
                    CareContent = model.CareContent,
                    Memo = model.Memo,
                    SendTime1 = model.SendTime1,
                    SendTime2 = model.SendTime2,
                    SendTime3 = model.SendTime3,
                    SendTime4 = model.SendTime4

                };


                db.CareElderItems.Add(careElderItem);
                db.SaveChanges();
                return RedirectToAction("Index", "CareElderItems", new { SubjectType = careElderItem.SubjectType, PSId = model.PSId });
            }

            return View(model);
        }

        // GET: CareElderItems/Create
        public ActionResult CreateExercising(string PSId)
        {
            ViewBag.PSId = PSId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExercising(CareElderExercisingViewModel model)
        {
            if (ModelState.IsValid)
            {

                var careElderItem = new CareElderItem
                {
                    PSId = model.PSId,
                    Sender = model.Sender,
                    SubjectType = model.SubjectType,
                    CareSubject = model.CareSubject,
                    ContentType = model.ContentType,
                    CareContent = model.CareContent,
                    Memo = model.Memo,
                    SendTime1 = model.SendTime1,
                    SendTime2 = model.SendTime2,
                    SendTime3 = model.SendTime3,
                    SendTime4 = model.SendTime4

                };


                db.CareElderItems.Add(careElderItem);
                db.SaveChanges();
                return RedirectToAction("Index", "CareElderItems", new { SubjectType = careElderItem.SubjectType, PSId = model.PSId });
            }

            return View(model);
        }

        // GET: CareElderItems/Create
        public ActionResult CreateMedicineRecord(string subject, string PSId)
        {
            ViewBag.Subject = subject;
            ViewBag.PSId = PSId;
            var careContent = from s in db.CareElderItems.ToList()
                              where s.PSId == PSId && s.SubjectType == "用藥提醒"
                              select s.CareContent;

            if (careContent.Count() > 0) {
                ViewBag.CareContent = careContent.First().Trim();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMedicineRecord(CareElderItemRecordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var careElderItemRecord = new CareElderItemRecord
                {
                    PSId = model.PSId,
                    SubjectType = model.SubjectType,
                    Subject = model.Subject,
                    ImageUrl = "",
                    Result = model.Result,
                    CreateDateTime = model.CreateDateTime
                };

                db.CareElderItemRecords.Add(careElderItemRecord);
                db.SaveChanges();
                return RedirectToAction("CareElderItemRecordsIndex", "CareElderItems", new { SubjectType = "已紀錄" });
            }

            return View(model);
        }

        public ActionResult CareElderItemRecordsIndex(string subjectType,string PSId) {
            ViewBag.SubjectType = subjectType;
            ViewBag.PSId = PSId;
            IEnumerable<CareElderItemRecord> model = db.CareElderItemRecords.Where(c => c.SubjectType.Trim() == subjectType && c.PSId == PSId).ToList();
            if (string.IsNullOrEmpty(subjectType)) {
                model = db.CareElderItemRecords.ToList();
            }

            return View(model);
        }

        // GET: CareElderItems/Create
        public ActionResult CreateEatingRecord(string PSId)
        {
            //ViewBag.Subject = subject;
            ViewBag.PSId = PSId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEatingRecord(CareElderEatingRecordViewModel model)
        {
            if (ModelState.IsValid)
            {

                string imageUrl = "";
                if(model.ImageUpload != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fileExt = Path.GetExtension(model.ImageUpload.FileName);
                    string filePath = Path.Combine(Server.MapPath("/uploads"), fileName + fileExt);
                    imageUrl = "/uploads/" + fileName + fileExt;
                    model.ImageUpload.SaveAs(filePath);
                }

                var careElderItemRecord = new CareElderItemRecord
                {
                    PSId = model.PSId,
                    SubjectType = model.SubjectType,
                    Subject = model.Subject,
                    ImageUrl = imageUrl,
                    Result = model.Result,
                    CreateDateTime = model.CreateDateTime
                };

                db.CareElderItemRecords.Add(careElderItemRecord);
                db.SaveChanges();
                return RedirectToAction("CareElderItemRecordsIndex", "CareElderItems", new { SubjectType = model.SubjectType });
            }

            return View(model);
        }

        // GET: CareElderItems/Create
        public ActionResult CreateExercisingRecord( string PSId)
        {
            //ViewBag.Subject = subject;
            ViewBag.PSId = PSId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExercisingRecord(CareElderExercisingRecordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var careElderItemRecord = new CareElderItemRecord
                {
                    PSId = model.PSId,
                    SubjectType = model.SubjectType,
                    Subject = model.Subject,
                    ImageUrl = "",
                    Result = model.Result,
                    CreateDateTime = model.CreateDateTime
                };

                db.CareElderItemRecords.Add(careElderItemRecord);
                db.SaveChanges();
                return RedirectToAction("CareElderItemRecordsIndex", "CareElderItems", new { SubjectType = model.SubjectType });
            }

            return View(model);
        }

        public ActionResult QueryMedicine()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QueryMedicine(string EName,string CName)
        {
            IQueryable<CareElderMedicine> result;
            if (!string.IsNullOrEmpty(EName))
                result = db.CareElderMedicines2.Where(c => c.EName.StartsWith(EName));
            else
                result = db.CareElderMedicines2;

            if (!string.IsNullOrEmpty(CName))
                result = result.Where(c => c.CName.StartsWith(CName));


            ViewBag.Medicines = result.ToList();
            
            return View();
        }

        public ActionResult QueryVideo()
        {

            ViewBag.VideoArray = videoEduList.ToArray();

            return View();
        }

        [HttpPost]
        public ActionResult QueryVideo(string videoCategory)
        {
            ViewBag.VideoCategory = videoCategory;
            switch (videoCategory) {
                case "惡性腫瘤":
                    ViewBag.VideoArray = videoEduList1.ToArray();
                    break;
                case "心臟疾病":
                    ViewBag.VideoArray = videoEduList.ToArray();
                    break;
                default:
                    ViewBag.VideoArray = videoEduList.ToArray();
                    break;
            }


            return View();
        }



        public ActionResult ListPlace(string lat, string lng)
        {
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;

            return View();
        }

    }
}
