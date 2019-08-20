using Jupiter._24Crafts.Data.Dtos.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Portfolio.Interface
{
   public interface IPortfolioBLL
    {
        object searchProfiles(SearchProfileRequestDto searchObj);
        void createPortfolio(PortfolioDto profile);
        void createBusinessProfie(PortfolioDto profile);
        object getProfileByUserId(long userId);
        IEnumerable<ProfessionDto> getAllProfessions(bool isAdmin);
        IEnumerable<ProfessionDto> getAllProfessionsByUserId(long userId);
        IEnumerable<LanguageDto> getAllLanguages(bool isAdmin);
        IEnumerable<LanguageDto> getAllLanguagesByUserId(long userId);
        bool Validateprofile(string mobileNum, int profileType);
        bool ValidateEmail(string mobileNum, string emailid, int profileType, long userId);
        bool SaveProfession(ProfessionDto profession);
        bool DeleteProfession(int professionId);
        bool SaveLanguage(LanguageDto language);
        bool DeleteLanguage(int languageId);
    }
}
