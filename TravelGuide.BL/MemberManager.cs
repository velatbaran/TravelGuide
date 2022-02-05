using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using TravelGuide.BL.ManagerBase;
using TravelGuide.BL.Results;
using TravelGuide.Entities.Entity;
using TravelGuide.Entities.Messages;
using TravelGuide.Entities.ValueObjects;

namespace TravelGuide.BL
{
    public class MemberManager : MyManagerBase<Member>
    {
        public BLResults<Member> blResult = new BLResults<Member>();

        public BLResults<Member> InsertMember(Member data)
        {
            blResult.Result = Find(x => x.Email == data.Email || x.Username == data.Username);
            if (blResult.Result != null)
            {
                if (blResult.Result.Email == data.Email)
                {
                    blResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu Email adresi zaten kullanılmaktadır.");
                }
                if (blResult.Result.Username == data.Username)
                {
                    blResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı zaten kullanılmaktadır.");
                }
            }
            else
            {
                data.ProfileImage = "user_boy.png";
                data.RoleId = 2;
                data.Password = Crypto.Hash(data.Password.ToString(), "MD5");
                data.RePassword = Crypto.Hash(data.RePassword.ToString(), "MD5");
                if (Insert(data) == 0)
                    blResult.AddError(ErrorMessageCode.MemberCouldNotInserted, "Kullanıcı kayıt edilirken hata oluştu.");
            }
            return blResult;
        }

        public BLResults<Member> GetMemberById(int? id)
        {
            blResult.Result = Find(x => x.Id == id.Value);
            if (blResult.Result == null)
            {
                blResult.AddError(ErrorMessageCode.MemberNotFound, "Üye bulunamadı!");
            }

            return blResult;
        }

        public BLResults<Member> UpdateMember(Member data)
        {
            blResult.Result = Find(x => x.Email == data.Email || x.Username == data.Username);
            if (blResult.Result != null)
            {
                if (blResult.Result.Email == data.Email)
                {
                    blResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu Email adresi zaten kullanılmaktadır.");
                }
                if (blResult.Result.Username == data.Username)
                {
                    blResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı zaten kullanılmaktadır.");
                }
            }
            else
            {
                blResult.Result = Find(x=>x.Id == data.Id);
                blResult.Result.Name = data.Name;
                blResult.Result.Surname = data.Surname;
                blResult.Result.Username = data.Username;
                blResult.Result.Email = data.Email;

                if (Update(blResult.Result) == 0)
                    blResult.AddError(ErrorMessageCode.MemberCouldNotUpdated, "Üye bilgileri güncellenirken hata oluştu.");
            }
            return blResult;
        }

        public BLResults<Member> DeleteMember(int? id)
        {
            blResult.Result = Find(x => x.Id == id.Value);
            if (blResult.Result == null)
            {
                blResult.AddError(ErrorMessageCode.MemberNotFound, "Üye bulunamadı!");
                return blResult;
            }

            if(Delete(blResult.Result) == 0)
            {
                blResult.AddError(ErrorMessageCode.MemberCouldNotDelete, "Üye silinirken hata oluştu.");
            }

            return blResult;
        }

        public BLResults<Member> ChangeRole(ChangeRoleValueObject model)
        {
            blResult.Result = Find(x => x.Id == model.MemberId);
            if (blResult.Result == null)
            {
                blResult.AddError(ErrorMessageCode.MemberNotFound, "Üye bulunamadı!");
                return blResult;
            }

            blResult.Result.RoleId = model.RoleId;

            if (Update(blResult.Result) == 0)
            {
                blResult.AddError(ErrorMessageCode.MemberCouldNotDelete, "Üye rolü değiştirilirken hata oluştu.");
            }

            return blResult;
        }

        public BLResults<Member> ChangePassword(ChangePasswordValueObject model,int id)
        {
            blResult.Result = Find(x => x.Id == id);
            if (blResult.Result == null)
            {
                blResult.AddError(ErrorMessageCode.MemberNotFound, "Üye bulunamadı!");
            }
            else
            {
                blResult.Result.Password = Crypto.Hash(model.Password.ToString(), "MD5");
                blResult.Result.RePassword = Crypto.Hash(model.RePassword.ToString(), "MD5");
                blResult.Result.PasswordResetDate = DateTime.Now;
                if (Update(blResult.Result) == 0)
                    blResult.AddError(ErrorMessageCode.MemberCouldNotInserted, "Şifre Değiştirilirken hata oluştu.");
            }
            return blResult;
        }

        public BLResults<Member> RemoveProfile(int id)
        {
            blResult.Result = Find(x => x.Id == id);
            if (blResult.Result == null)
            {
                blResult.AddError(ErrorMessageCode.MemberNotFound, "Üye bulunamadı!");
            }
            else
            {
                blResult.Result.IsDeleted = true;
                blResult.Result.DeletedOn = DateTime.Now;
                if (Delete(blResult.Result) == 0)
                    blResult.AddError(ErrorMessageCode.MemberCouldNotInserted, "Üye silinirken hata oluştu.");
            }
            return blResult;
        }

        public BLResults<Member> UpdateProfile(Member data)
        {
            blResult.Result = Find(x => x.Email == data.Email || x.Username == data.Username);
            if (blResult.Result != null)
            {
                if (blResult.Result.Email == data.Email)
                {
                    blResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu Email adresi zaten kullanılmaktadır.");
                }
                if (blResult.Result.Username == data.Username)
                {
                    blResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı zaten kullanılmaktadır.");
                }
            }
            else
            {
                blResult.Result = Find(x => x.Id == data.Id);
                blResult.Result.Name = data.Name;
                blResult.Result.Surname = data.Surname;
                blResult.Result.Username = data.Username;
                blResult.Result.Email = data.Email;
                if (string.IsNullOrEmpty(data.ProfileImage) == false)
                {
                    blResult.Result.ProfileImage = data.ProfileImage;
                }
                if (Update(blResult.Result) == 0)
                    blResult.AddError(ErrorMessageCode.MemberCouldNotUpdated, "Profil bilgileri güncellenirken hata oluştu.");
            }
            return blResult;
        }
    }
}
