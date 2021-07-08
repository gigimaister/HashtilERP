using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HashtilERP.Data;
using Microsoft.AspNetCore.Identity;
using HashtilERP.Server.Models;

namespace HashtilERP.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPassportsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HashtilERPContext _context;

        public KPassportsController(HashtilERPContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region GET
        //GET WITH PASSPORT STATUS CONDITION
        [HttpGet("multi/{status}")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetKPassport(string status)
        {
            var ChosenList = new List<K_Passport>();
            var user = await _userManager.GetUserAsync(User);

            try
            {
                switch (status)
                {
                    //growing room
                    case "1":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.GrowingRoom)
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .OrderByDescending(x => x.KPassportInsertAudit.Date)
                            .ToListAsync();
                        break;
                    //waiting for confirmation
                    case "2":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.WaitingForOK)
                           .ToListAsync();
                        break;
                    //inside green house
                    case "3":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatusCode == (int)PassportStatusCode.InsideGreenHouse)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    //need to be audit
                    case "4":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsNeedToBeAudit == true)
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    //need to be checked
                    case "5":
                        ChosenList = await _context.KPassport.Where(x => x.IsNeedToBeChecked == true)
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    //for AVG Counter
                    case "6":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus.Trim() == Status.InGreenHouse && x.PassportAvg != -1)
                           .OrderByDescending(x => x.AVGEnteringDate)
                           .ToListAsync();
                        break;
                    //archive
                    case "7":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus.Trim() == Status.Destroyed || x.PassportStatus.Trim() == Status.Finished && x.IsNeedToBeChecked == false)
                           .OrderByDescending(x => x.AVGEnteringDate)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;


                }
                return ChosenList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //ForThai-Guy
        [HttpGet("GetGrowingPassForSowing")]
        public async Task<List<K_Passport>> GetGrowingPassForSowing()
        {
            var passports = new List<K_Passport>();
            passports = await _context.KPassport.Where(x => x.PassportStatus == Status.GrowingRoom).OrderByDescending(x=>x.PassportNum).ToListAsync();
            return passports;
        }

        //Metzay
        [HttpGet("ForKOrderJobs")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetPassportsForKOrderJobs()
        {
            var passports = new List<K_Passport>();
            try
            {
                passports = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return passports;
        }

        //Metzay
        [HttpGet("metzay")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetCurrentMetzay()
        {
            var metzay = new List<K_Passport>();


            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //Metzay Light
        [HttpGet("metzayLight")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetCurrentMetzayLight()
        {
            var metzay = new List<K_Passport>();
            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse).ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Tomatos cut
        [HttpGet("NeedToCutt")]
        public async Task<List<K_Passport>> GetNeedToCutt()
        {
            var passports = new List<K_Passport>();
            try
            {
                passports = await _context.KPassport.Where(x => x.IsNeedToCut == true && x.PassportStatus == Status.InGreenHouse).ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return passports;
        }
        //Tomatos For cut
        [HttpGet("NeedToBeCut")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetNeedToBeCut()
        {
            var metzay = new List<K_Passport>();
            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsNeedToCut == true)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //Saved For cx
        [HttpGet("savedforcx")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetSavedForCx()
        {
            var metzay = new List<K_Passport>();
            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsSavedForCx == true)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //GET for LowAVG 
        [HttpGet("lowavg")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetLoeAVGMetzay()
        {
            var metzay = new List<K_Passport>();


            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsLowAVG == true)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //List Of Passports For Metzay Report for day...
        [HttpGet("report/{date}")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetMetzayReportStatus(string date)
        {
            var ChosenList = new List<K_Passport>();


            try
            {
                ChosenList = await _context.KPassport.Where(x => x.MetzayEnteringDate < Convert.ToDateTime(date) && Convert.ToDateTime(date) < x.MetzayOutGoingDate)

                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Oitm)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.UpdateK_PassportAudit)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();

                return ChosenList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //GET FOR OBJECT RETURN TO RAZOR PAGE
        [HttpGet("Thai/GreenHouse/{id}")]
        public async Task<K_Passport> GetThaiKPassport(int id)
        {
            var kPassport = new K_Passport();
            try
            {
                kPassport = await _context.KPassport.Where(x => x.PassportNum == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return kPassport;
        }


        // GET: api/KPassports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<K_Passport>> GetKPassport(int id)
        {

            var kPassport = await _context.KPassport.FindAsync(id);

            if (kPassport == null)
            {
                return NotFound();
            }

            return kPassport;
        }

        //KPassports For Korder By ItemCode
        [HttpGet("GetKPassportsForKOrderByItemCode/{itemcode}")]
        public async Task<List<K_Passport>> GetKPassportsForKOrder(string itemcode)
        {
            var k_Passports = new List<K_Passport>();

            k_Passports = await _context.KPassport.Where(x => x.ItemCode == itemcode && x.PassportStatus == Status.InGreenHouse)
                 .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .OrderBy(x => x.SowDate)
                .ToListAsync();

            return k_Passports;
        }

        //KPassports For Korder By Gidul && Zan
        [HttpGet("GetKPassportsForKOrderByGidulAndZan/{gidul}/{zan}")]
        public async Task<List<K_Passport>> GetKPassportsForKOrderByGidulAndZan(string gidul, string zan)
        {
            var k_Passports = new List<K_Passport>();
            k_Passports = await _context.KPassport.Where(x => x.Gidul.Contains(gidul) && x.Zan.Contains(zan) && x.PassportStatus == Status.InGreenHouse)
                .Include(e => e.Passport)
                .ThenInclude(e => e.Passprods)
                .Include(e => e.PassportAuditForms)
                .Include(e => e.k_PassportAuditTblVer2s)
                .OrderBy(x => x.SowDate)
                .ToListAsync();
            return k_Passports;
        }
        //KPassports For Korder By Gidul && Zan
        [HttpGet("GetKPassportsForKOrderByGidulZan/{gidul}/{zan}")]
        public async Task<List<K_Passport>> GetKPassportsForKOrderByGidulZan(string gidul, string zan)
        {
            var k_Passports = new List<K_Passport>();

            k_Passports = await _context.KPassport.Where(x => x.Gidul == gidul && x.Zan == zan).OrderBy(x => x.SowDate).ToListAsync();

            return k_Passports;
        }

        [HttpGet("AuditPassports")]
        public async Task<List<K_Passport>> AuditPassports()
        {
            var ChosenList = new List<K_Passport>();
            try
            {
                ChosenList = await _context.KPassport.Where(x => (x.PassportStatus == Status.InGreenHouse) && (x.HasBeenAudited == false)).OrderBy(x => x.SowDate).ToListAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ChosenList;
        }

        //Sowing GET for Insert new passport
        [HttpGet("GetInsertPassport/{passNum}/{growingRoom}")]
        public async Task<ActionResult> GetInsertPassport(int? passNum,string growingRoom)
        {
            var kPassport = new K_Passport();
            Passport sap;
            Oitm sapOitm;
            int? startingAvg;
            try
            {
                sap = await _context.Passport.Where(X => X.DocNum == passNum).FirstAsync();
                sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();


            }
            //if no passport in SAP
            catch (Exception)
            {
                return StatusCode(500, "NOTFOUND");
            }


            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;

            var dup = await _context.KPassport.Where(X => X.PassportNum == passNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return StatusCode(500, "DUPLICATE");
            }
            DateTime passingDate;
            kPassport.UserName = screenName.Trim();
            kPassport.SowDate = sap.UDateSow;
            passingDate = (DateTime)sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            //if ZIREY LAKOACH
            if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
            kPassport.PassportStartingAVG = startingAvg;
            kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd * 1000;
            kPassport.PassportStatus = Status.GrowingRoom.Trim();
            kPassport.PassportStatusCode = (int)PassportStatusCode.Growingroom;
            kPassport.ItemCode = sap.UItemCode.Trim();
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked.Trim();
            kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
            kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
            kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
            kPassport.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
            kPassport.IsSavedForCx = false;
            kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
            kPassport.PassportNum = passNum;
            kPassport.GrowingRoom = growingRoom;
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (KPassportExists(kPassport.K_PassportId))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.K_PassportId }, kPassport);
        }

        //Mangers GET for Insert new passport(If Sowing did NOT scan at growingRooms)
        [HttpGet("GetInsertPassportManager/{passNum}/{hamama}/{gamlon}")]
        public async Task<ActionResult> GetInsertPassportManager(int? passNum, string hamama, string gamlon)
        {
            var kPassport = new K_Passport();
            Passport sap;
            Oitm sapOitm;
            int? startingAvg;
            try
            {
                sap = await _context.Passport.Where(X => X.DocNum == passNum).FirstAsync();
                sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();


            }
            //if no passport in SAP
            catch (Exception)
            {
                return StatusCode(500, "NOTFOUND");
            }


            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;

            var dup = await _context.KPassport.Where(X => X.PassportNum == passNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return StatusCode(500, "DUPLICATE");
            }
            DateTime passingDate;
            kPassport.UserName = screenName.Trim();
            kPassport.SowDate = sap.UDateSow;
            passingDate = (DateTime)sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            //if ZIREY LAKOACH
            if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
            kPassport.PassportStartingAVG = startingAvg; kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd * 1000;
            kPassport.PassportStatus = Status.InGreenHouse.Trim();
            kPassport.PassportStatusCode = (int)PassportStatusCode.InsideGreenHouse;
            kPassport.ItemCode = sap.UItemCode.Trim();
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked.Trim();
            kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
            kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
            kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
            kPassport.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
            kPassport.IsSavedForCx = false;
            kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
            kPassport.PassportNum = passNum;
            kPassport.GrowingRoom = "X";
            kPassport.Hamama = hamama;
            kPassport.Gamlon = gamlon;
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (KPassportExists(kPassport.K_PassportId))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.K_PassportId }, kPassport);
        }

        [HttpGet("MetzayForOren")]
        public async Task<List<MetzayForOren>> MetzayForOren()
        {
            var K_Passports = new List<K_Passport>();
            var metzayForOrens = new List<MetzayForOren>();


            try
            {
                K_Passports = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse)                        
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)                           
                           .ToListAsync();

                //for every passport
                foreach (var kpassport in K_Passports)
                {
                    decimal? temp;
                    int startAvg;
                    if (kpassport.Passport.Passprods.Count == 0)
                    {
                        try
                        {
                            temp = kpassport.Passport.UQuanProd * 1000;
                            startAvg = Convert.ToInt32((kpassport.Passport.UQuanProd * 1000) / kpassport.Passport.UTraySow);
                            MetzayForOren metzayForOren = new MetzayForOren
                            {
                                Hamama = kpassport.Hamama,
                                Gamlon = kpassport.Gamlon,
                                Gidul = kpassport.Gidul,
                                Zan = kpassport.Zan,
                                SowDate = kpassport.SowDate,
                                DateEnd = kpassport.DateEnd,
                                GrowingDays = kpassport.GrowingDays,
                                PassportNum = kpassport.PassportNum,
                                Cx = "לא הוכנס ל-SAP",
                                MagashAmount = kpassport.MagashAmount,
                                PassportAvg = kpassport.PassportAvg,
                                CxPlantsOrder = temp,
                                CelsTray = kpassport.CelsTray,
                                PassportStartingAVG = startAvg,
                                SumOfCxsPlants = Convert.ToInt32(temp)
                            };
                            metzayForOrens.Add(metzayForOren);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                    }
                    //if more than 1 cx
                    else if (kpassport.Passport.Passprods.Count > 1)
                    {
                        var counter = 1;
                        var sumofPlants = 0;

                        foreach (var cx in kpassport.Passport.Passprods)
                        {
                            if (kpassport.Passport.UQuanOrdP > 5555554)
                            {
                                temp = kpassport.Passport.UQuanProd * 1000;
                                startAvg = Convert.ToInt32((kpassport.Passport.UQuanProd * 1000) / kpassport.Passport.UTraySow);
                            }
                            else
                            {
                                temp = (cx.UQuantity * 1000);
                                startAvg = Convert.ToInt32((kpassport.Passport.UQuanOrdP * 1000) / kpassport.Passport.UTraySow);
                            }
                            sumofPlants += Convert.ToInt32(temp);
                            counter++;
                            MetzayForOren metzayForOren = new MetzayForOren
                            {
                                Hamama = kpassport.Hamama,
                                Gamlon = kpassport.Gamlon,
                                Gidul = kpassport.Gidul,
                                Zan = kpassport.Zan,
                                SowDate = kpassport.SowDate,
                                DateEnd = kpassport.DateEnd,
                                GrowingDays = kpassport.GrowingDays,
                                PassportNum = kpassport.PassportNum,
                                Cx = cx.UCustName,
                                PassportAvg = kpassport.PassportAvg,
                                CxPlantsOrder = temp,
                                CelsTray = kpassport.CelsTray,
                                PassportStartingAVG = startAvg


                            };
                            if (counter > kpassport.Passport.Passprods.Count)
                            {
                                metzayForOren.SumOfCxsPlants = sumofPlants;
                                metzayForOren.MagashAmount = kpassport.MagashAmount;
                            }
                            metzayForOrens.Add(metzayForOren);
                        }
                    }
                    //if cx list = 1
                    else
                    {

                        if (kpassport.Passport.UQuanOrdP > 5555554)
                        {
                            temp = kpassport.Passport.UQuanProd * 1000;
                            startAvg = Convert.ToInt32((kpassport.Passport.UQuanProd * 1000) / kpassport.Passport.UTraySow);
                        }
                        else
                        {
                            temp = (kpassport.Passport.Passprods.FirstOrDefault().UQuantity * 1000);
                            startAvg = Convert.ToInt32((kpassport.Passport.UQuanOrdP * 1000) / kpassport.Passport.UTraySow);
                        }

                        MetzayForOren metzayForOren = new MetzayForOren
                        {
                            Hamama = kpassport.Hamama,
                            Gamlon = kpassport.Gamlon,
                            Gidul = kpassport.Gidul,
                            Zan = kpassport.Zan,
                            SowDate = kpassport.SowDate,
                            DateEnd = kpassport.DateEnd,
                            GrowingDays = kpassport.GrowingDays,
                            PassportNum = kpassport.PassportNum,
                            Cx = kpassport.Passport.Passprods.FirstOrDefault().UCustName,
                            MagashAmount = kpassport.MagashAmount,
                            PassportAvg = kpassport.PassportAvg,
                            CxPlantsOrder = temp,
                            CelsTray = kpassport.CelsTray,
                            PassportStartingAVG = startAvg,
                            SumOfCxsPlants = Convert.ToInt32(temp)
                        };
                        metzayForOrens.Add(metzayForOren);
                    }
                }
                return metzayForOrens;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region PUT
        // PUT: GREENHOUSE THAI HAMAMA AND GAMLON UPDATE       
        [HttpPut("Thai/Passport/Update/{id}")]
        public async Task<IActionResult> PutThaiKPassport(int id, K_Passport kPassport)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kPassport.UserName = screenName;

            if (id != kPassport.K_PassportId)
            {
                return BadRequest();
            }


            _context.Entry(kPassport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception e)
            {
                if (!KPassportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return Ok();
        }

        // PUT: GREENHOUSE MANAGER STATUS TO CONFIRMED

        [HttpPut("GreenManager/Passport/UpdateStatus/{id}")]
        public async Task<IActionResult> PutGreenManagerKPassportStatus(int id, K_Passport kPassport)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kPassport.UserName = screenName;

            if (id != kPassport.K_PassportId)
            {
                return BadRequest();
            }


            _context.Entry(kPassport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception e)
            {
                if (!KPassportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return Ok();
        }


        // PUT: api/KPassports/5
        //PUT DEFAULT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKPassport(int id, K_Passport kPassport)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kPassport.UserName = screenName;

            if (id != kPassport.K_PassportId)
            {
                return BadRequest();
            }

            _context.Entry(kPassport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!KPassportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("SetKPassportNeedToCheckON/{id}")]
        public async Task<IActionResult> SetKPassportNeedToCheckON(int id, K_Passport kPassport)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kPassport.UserName = screenName;
            kPassport.K_PassportId = id;

            if (id != kPassport.K_PassportId)
            {
                return BadRequest();
            }

            _context.Entry(kPassport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!KPassportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        #endregion

        #region POST
        // POST: api/KPassports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SowInsert")]
        public async Task<ActionResult<K_Passport>> PostKPassport(K_Passport kPassport)
        {
            Passport sap;
            Oitm sapOitm;
            int? startingAvg;
            try
            {
                sap = await _context.Passport.Where(X => X.DocNum == kPassport.PassportNum).FirstAsync();
                sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();


            }
            //if no passport in SAP
            catch (Exception)
            {
                return StatusCode(500, "NOTFOUND");
            }


            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;

            var dup = await _context.KPassport.Where(X => X.PassportNum == kPassport.PassportNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return StatusCode(500, "DUPLICATE");
            }
            DateTime passingDate;
            kPassport.UserName = screenName.Trim();
            kPassport.SowDate = sap.UDateSow;
            passingDate = (DateTime)sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            //if ZIREY LAKOACH
            if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
            kPassport.PassportStartingAVG = startingAvg; kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd * 1000;
            kPassport.PassportStatus = Status.GrowingRoom.Trim();
            kPassport.PassportStatusCode = (int)PassportStatusCode.Growingroom;
            kPassport.ItemCode = sap.UItemCode.Trim();
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked.Trim();
            kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
            kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
            kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
            kPassport.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
            kPassport.IsSavedForCx = false;
            kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (KPassportExists(kPassport.K_PassportId))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.K_PassportId }, kPassport);
        }

        [HttpPost("NewPassportInsertByManager")]
        public async Task<ActionResult<K_Passport>> PostNewKPassportByManager(K_Passport kPassport)
        {
            Passport sap;
            Oitm sapOitm;
            try
            {
                sap = await _context.Passport.Where(X => X.DocNum == kPassport.PassportNum).FirstAsync();
                sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();
            }
            //if no passport in SAP
            catch (Exception)
            {
                return StatusCode(500, "NOTFOUND");
            }


            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            int? startingAvg;

            var dup = await _context.KPassport.Where(X => X.PassportNum == kPassport.PassportNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return StatusCode(500, "DUPLICATE");
            }
            DateTime passingDate;
            kPassport.UserName = screenName.Trim();
            kPassport.SowDate = sap.UDateSow;
            passingDate = (DateTime)sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            //if ZIREY LAKOACH
            if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
            kPassport.PassportStartingAVG = startingAvg;
            kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd * 1000;
            kPassport.PassportStatus = Status.InGreenHouse.Trim();
            kPassport.PassportStatusCode = (int)PassportStatusCode.InsideGreenHouse;
            kPassport.ItemCode = sap.UItemCode.Trim();
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked.Trim();
            kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
            kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
            kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
            kPassport.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
            kPassport.IsSavedForCx = false;
            kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (KPassportExists(kPassport.K_PassportId))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.K_PassportId }, kPassport);
        }

        [HttpPost("InsertUpdatePassport/{passportNum}/{growingRoom}")]
        public async Task<int> InsertUpdatePassport(int? passportNum,string growingRoom)
        {
            Passport sap;
            Oitm sapOitm;
            try
            {
                sap = await _context.Passport.Where(X => X.DocNum == passportNum).FirstAsync();
                sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();
                var user = await _userManager.GetUserAsync(User);
                var screenName = user.ScreenName;
                int? startingAvg;
                var dup = await _context.KPassport.Where(X => X.PassportNum == passportNum).FirstOrDefaultAsync();
                //if duplicate in K_Passport
                if (dup != null)
                {
                    return 1;
                }
                var kPassport = new K_Passport();
                DateTime passingDate;
                kPassport.GrowingRoom = 
                kPassport.UserName = screenName.Trim();
                kPassport.SowDate = sap.UDateSow;
                passingDate = (DateTime)sap.UDateSow;
                kPassport.DateEnd = sap.UDateEnd;
                //if ZIREY LAKOACH
                if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
                kPassport.PassportStartingAVG = startingAvg;
                kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
                kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
                kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
                kPassport.PlantsAmount = sap.UQuanProd * 1000;
                kPassport.PassportStatus = Status.InGreenHouse.Trim();
                kPassport.PassportStatusCode = (int)PassportStatusCode.InsideGreenHouse;
                kPassport.ItemCode = sap.UItemCode.Trim();
                kPassport.SapDocEntry = sap.DocEntry;
                kPassport.PassportCondition = Status.NotChecked.Trim();
                kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
                kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
                kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
                kPassport.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
                kPassport.IsSavedForCx = false;
                kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
                _context.KPassport.Add(kPassport);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    if (KPassportExists(kPassport.K_PassportId))
                    {
                        return 1;
                    }
                    else
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
            //if no passport in SAP
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return 0;
        }

        #endregion

        #region DELETE
        // DELETE: api/KPassports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKPassport(int id)
        {
            var kPassport = await _context.KPassport.FindAsync(id);
            if (kPassport == null)
            {
                return NotFound();
            }

            _context.KPassport.Remove(kPassport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KPassportExists(int id)
        {
            return _context.KPassport.Any(e => e.K_PassportId == id);
        }


        #endregion
    }

}
