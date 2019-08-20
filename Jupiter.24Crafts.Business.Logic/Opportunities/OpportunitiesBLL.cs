using Jupiter._24Crafts.Business.Logic.Opportunities.Interface;
using Jupiter._24Crafts.Data.Dtos.Opportunities;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Opportunities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Jupiter._24Crafts.Business.Logic.Opportunities
{
    public class OpportunitiesBLL : IOpportunitiesBLL
    {
        private readonly IOpportunitiesUnitOfWork _oUoW;
        private readonly ILogExceptionUnitOfWork _logUoW;

        public OpportunitiesBLL(IOpportunitiesUnitOfWork unitOfWork, ILogExceptionUnitOfWork logUnitOfwork)
        {
            this._oUoW = unitOfWork;
            this._logUoW = logUnitOfwork;
        }

        public IEnumerable<OpportunityDto> searchOpportunities(SearchOpportunityRequestDto searchObj)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.searchOpportunities(searchObj);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool createOpportunity(OpportunityDto opportunity)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.createOpportunity(opportunity);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool updateOpportunity(OpportunityDto opportunity)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.updateOpportunity(opportunity);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<OpportunityDto> getMyOpportunities(long userId)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.getMyOpportunities(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public int getOpportunitiesNotification(long userId)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.getOpportunitiesNotification(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<OpportunityDto> getOpportunities(long userId)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.getOpportunities(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<OpportunityDto> getMyAppliedOpportunities(long userId)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.getMyAppliedOpportunities(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool applyOpportunity(ApplyOpportunityDto opportunityObj)
        {
            try
            {
               return _oUoW.OpportunitiesRepository.applyOpportunity(opportunityObj);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool deleteOpportunity(int opportunityId)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.deleteOpportunity(opportunityId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<OpportunityResponseDto> getRespondedUsersForMyOpportunities(long userId)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.getRespondedUsersForMyOpportunities(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }


        public IEnumerable<OpportunityDto> AllOpportunitesForApproval()
        {
            try
            {
                return _oUoW.OpportunitiesRepository.AllOpportunitesForApproval();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool ApproveOrRejectOpportunity(long IsApprovedBy, int OpportunityId, bool IsApproved, string IsApprovedComments)
        {
            try
            {
                return _oUoW.OpportunitiesRepository.ApproveOrRejectOpportunity(IsApprovedBy, OpportunityId, IsApproved, IsApprovedComments);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
    }
}
