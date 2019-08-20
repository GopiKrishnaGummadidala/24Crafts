using Jupiter._24Crafts.Data.Dtos.Opportunities;
using Jupiter._24Crafts.Data.Repositories.Opportunities.Interface;
using Jupiter._24Crafts.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jupiter._24Crafts.Common.Resources;

namespace Jupiter._24Crafts.Data.Repositories.Opportunities
{
    public class OpportunitiesRepository : IOpportunitiesRepository
    {
        private readonly CratfsDb _craftsDb;

        public OpportunitiesRepository(CratfsDb craftsDb)
        {
            this._craftsDb = craftsDb;
        }

        public IEnumerable<OpportunityDto> searchOpportunities(SearchOpportunityRequestDto searchObj)
        {
            var popularProfilesList = _craftsDb.Opportunities.Where(o => o.IsActive == true && (!string.IsNullOrEmpty(searchObj.Title) ? o.Title.Contains(searchObj.Title) : true) && (!string.IsNullOrEmpty(searchObj.Remuneration) ? o.Remuneration.Contains(searchObj.Remuneration) : true) && (searchObj.AvailableStartDate != null ? o.StartDate == searchObj.AvailableStartDate : true) && (searchObj.AvailableEndDate != null ? o.EndDate == searchObj.AvailableEndDate : true)).ToList();

            return popularProfilesList.Select(s =>
                 new OpportunityDto()
                 {
                     Title = s.Title,
                     Professions = s.Profession_Opportunity_Tie.Where(pot => pot.OpportunityId == s.OpportunityId).Select(res => new Professions { Id = res.Profession.ProfessionId, ProfessionName = res.Profession.ProfessionName }).ToList(),
                     Remuneration = s.Remuneration,
                     Description = s.Description,
                     StartDate = s.StartDate,
                     EndDate = s.EndDate,
                     CreatedByUserId = s.CreatedByUserId
                 }).Take(10).ToList();
        }

        public bool createOpportunity(OpportunityDto opportunityDto)
        {
            try
            {
                if (!IsSpamDataContains.IsValidData(opportunityDto.Description))
                {
                    Opportunity opt = new Opportunity()
                    {
                        Title = opportunityDto.Title,
                        Description = opportunityDto.Description,
                        Remuneration = opportunityDto.Remuneration,
                        StartDate = opportunityDto.StartDate,
                        EndDate = opportunityDto.EndDate,
                        CreatedByUserId = opportunityDto.CreatedByUserId,
                        IsActive = true
                    };
                    _craftsDb.Opportunities.Add(opt);
                    _craftsDb.SaveChanges();

                    var opportunityId = _craftsDb.Opportunities.OrderByDescending(o => o.OpportunityId).FirstOrDefault().OpportunityId;

                    foreach (var professionId in opportunityDto.ProfessionIds.Split(',').Select(sValue => sValue.Trim()).ToArray())
                    {
                        Profession_Opportunity_Tie pot = new Profession_Opportunity_Tie();
                        pot.CreatedBy_UserID = opportunityDto.CreatedByUserId;
                        pot.ProfessionID = Convert.ToInt32(professionId);
                        pot.OpportunityId = opportunityId;
                        pot.IsActive = true;

                        _craftsDb.Profession_Opportunity_Tie.Add(pot);
                        _craftsDb.SaveChanges();
                    }

                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool updateOpportunity(OpportunityDto opportunityDto)
        {
            try
            {
                if (!IsSpamDataContains.IsValidData(opportunityDto.Description))
                {
                    var opportunity = _craftsDb.Opportunities.Where(o => o.OpportunityId == opportunityDto.OpportunityId).FirstOrDefault();
                    if (opportunity != null)
                    {
                        opportunity.Title = opportunityDto.Title;
                        opportunity.Remuneration = opportunityDto.Remuneration;
                        opportunity.StartDate = opportunityDto.StartDate;
                        opportunity.EndDate = opportunityDto.EndDate;
                        opportunity.Description = opportunityDto.Description;
                        opportunity.IsActive = opportunityDto.IsActive;

                        _craftsDb.SaveChanges();

                        var oppProfessions = _craftsDb.Profession_Opportunity_Tie.Where(pot => pot.OpportunityId == opportunityDto.OpportunityId).ToList();

                        _craftsDb.Profession_Opportunity_Tie.RemoveRange(oppProfessions);
                        _craftsDb.SaveChanges();

                        //foreach (var oppProfessionId in oppProfessions)
                        //{
                        //    _craftsDb.Profession_Opportunity_Tie.Remove(oppProfessionId);
                        //    _craftsDb.SaveChanges();
                        //}

                        foreach (var professionId in opportunityDto.ProfessionIds.Split(',').Select(sValue => sValue.Trim()).ToArray())
                        {
                            Profession_Opportunity_Tie pot = new Profession_Opportunity_Tie();
                            pot.CreatedBy_UserID = opportunityDto.CreatedByUserId;
                            pot.ProfessionID = Convert.ToInt32(professionId);
                            pot.OpportunityId = opportunityDto.OpportunityId;
                            pot.IsActive = true;

                            _craftsDb.Profession_Opportunity_Tie.Add(pot);
                            _craftsDb.SaveChanges();
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool applyOpportunity(ApplyOpportunityDto opportunityDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(opportunityDto.Description) && !_craftsDb.OpportunityResponse_User_Tie.Any(_ => _.UserId == opportunityDto.UserId && _.OpportunityId == opportunityDto.OpportunityId && _.IsActive == true))
                {
                    if (!IsSpamDataContains.IsValidData(opportunityDto.Description))
                    {
                        OpportunityResponse_User_Tie obj = new OpportunityResponse_User_Tie()
                        {
                            Description = opportunityDto.Description,
                            OpportunityId = opportunityDto.OpportunityId,
                            UserId = opportunityDto.UserId,
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        _craftsDb.OpportunityResponse_User_Tie.Add(obj);
                        _craftsDb.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<OpportunityDto> getMyOpportunities(long userId)
        {
            return _craftsDb.Opportunities.Where(o => o.CreatedByUserId == userId  && o.IsActive == true).Select(s =>
                  new OpportunityDto()
                  {
                      Title = s.Title,
                      OpportunityId = s.OpportunityId,
                      Professions = s.Profession_Opportunity_Tie.Where(pot => pot.OpportunityId == s.OpportunityId).Select(res => new Professions { Id = res.Profession.ProfessionId, ProfessionName = res.Profession.ProfessionName }).ToList(),
                      Remuneration = s.Remuneration,
                      Description = s.Description,
                      StartDate = s.StartDate,
                      EndDate = s.EndDate,
                      CreatedByUserId = s.CreatedByUserId
                  }).ToList();
        }

        public IEnumerable<OpportunityDto> getMyAppliedOpportunities(long userId)
        {
            return _craftsDb.OpportunityResponse_User_Tie.Where(o => o.UserId == userId && o.IsActive == true && o.Opportunity.IsActive == true).Select(s => new OpportunityDto()
            {
                Title = s.Opportunity.Title,
                OpportunityId = s.Opportunity.OpportunityId,
                Professions = s.Opportunity.Profession_Opportunity_Tie.Where(pot => pot.OpportunityId == s.OpportunityId).Select(res => new Professions { Id = res.Profession.ProfessionId, ProfessionName = res.Profession.ProfessionName }).ToList(),
                Remuneration = s.Opportunity.Remuneration,
                Description = s.Opportunity.Description,
                StartDate = s.Opportunity.StartDate,
                EndDate = s.Opportunity.EndDate,
                CreatedByUserId = s.Opportunity.CreatedByUserId
            }).ToList();
        }

        public IEnumerable<OpportunityDto> getOpportunities(long userId)
        {
            return _craftsDb.Opportunities.Where(_=>_.IsActive == true && _.CreatedByUserId != userId).Select(s =>
                              new OpportunityDto()
                              {
                                  Title = s.Title,
                                  OpportunityId = s.OpportunityId,
                                  Professions = s.Profession_Opportunity_Tie.Where(pot => pot.OpportunityId == s.OpportunityId).Select(res => new Professions { Id = res.Profession.ProfessionId, ProfessionName = res.Profession.ProfessionName }).ToList(),
                                  Remuneration = s.Remuneration,
                                  Description = s.Description,
                                  StartDate = s.StartDate,
                                  EndDate = s.EndDate,
                                  IsApplied = s.OpportunityResponse_User_Tie.Any(_ => _.UserId == userId),
                                  CreatedByUserId = s.CreatedByUserId
                              }).ToList().Take(10);
        }

        public bool deleteOpportunity(int opportunityId)
        {
            try
            {
                var deleteObj = _craftsDb.Opportunities.Where(o => o.OpportunityId == opportunityId).FirstOrDefault();
                if (deleteObj != null)
                {
                    //var potObj = _craftsDb.Profession_Opportunity_Tie.Where(pot => pot.OpportunityId == opportunityId).ToList();
                    //_craftsDb.Profession_Opportunity_Tie.RemoveRange(potObj);
                    //_craftsDb.SaveChanges();
                     
                    deleteObj.IsActive = false;
                    _craftsDb.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<OpportunityResponseDto> getRespondedUsersForMyOpportunities(long userId)
        {
            return (from opp in _craftsDb.Opportunities
                         join orut in _craftsDb.OpportunityResponse_User_Tie on opp.OpportunityId equals orut.OpportunityId
                         join user in _craftsDb.Users on orut.UserId equals user.UserID
                         where opp.CreatedByUserId == userId && opp.IsActive == true
                         select new OpportunityResponseDto { UserId = user.UserID, Title = opp.Title, CustomerId = user.CustomerId, Name = user.FirstName, Description = orut.Description }).ToList();
        }

        public IEnumerable<OpportunityDto> AllOpportunitesForApproval()
        {
            return (from opp in _craftsDb.Opportunities
                    join user in _craftsDb.Users on opp.CreatedByUserId equals user.UserID
                    where opp.IsActive == true
                    select new OpportunityDto { CustId = user.CustomerId, Name = user.FirstName, Description = opp.Description, OpportunityId = opp.OpportunityId, CreatedByUserId = user.UserID, Title = opp.Title }).ToList();
        }

        public bool ApproveOrRejectOpportunity(long IsApprovedBy,int OpportunityId, bool IsApproved, string IsApprovedComments)
        {
            var opp = _craftsDb.Opportunities.Where(o => o.OpportunityId == OpportunityId).FirstOrDefault();
            if (opp != null)
            {
                opp.IsApprovedBy = IsApprovedBy;
                opp.IsApproved = IsApproved;
                opp.IsApprovedComments = IsApprovedComments;
                _craftsDb.SaveChanges();
            }

            return true;
        }
        public int getOpportunitiesNotification(long userId)
        {
          return _craftsDb.Opportunities.Where(o => o.CreatedByUserId != userId && o.StartDate == DateTime.Today).Count();
        }
    }
}
