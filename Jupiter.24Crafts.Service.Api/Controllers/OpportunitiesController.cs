using Jupiter._24Crafts.Business.Logic.Opportunities.Interface;
using Jupiter._24Crafts.Data.Dtos.Opportunities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jupiter._24Crafts.Service.Api.Controllers
{
    [RoutePrefix("Opportunities")]
    public class OpportunitiesController : ApiController
    {
        private readonly IOpportunitiesBLL _opportunityBll;

        public OpportunitiesController()
        { }

        public OpportunitiesController(IOpportunitiesBLL bll)
        {
            _opportunityBll = bll;
        }

        [HttpGet]
        public IEnumerable<OpportunityDto> searchOpportunities([FromUri]SearchOpportunityRequestDto searchObj)
        {
            return _opportunityBll.searchOpportunities(searchObj);
        }

        [Route("getOpportunitiesNotification")]
        [HttpGet]
        public int getOpportunitiesNotification(long userId)
        {
            return _opportunityBll.getOpportunitiesNotification(userId);
        }

        [Route("getMyOpportunities")]
        [HttpGet]
        public IEnumerable<OpportunityDto> getMyOpportunities(long userId)
        {
            return _opportunityBll.getMyOpportunities(userId);
        }

        [Route("getOpportunities")]
        [HttpGet]
        public IEnumerable<OpportunityDto> getOpportunities(long userId=0)
        {
            return _opportunityBll.getOpportunities(userId);
        }

        [Route("getMyAppliedOpportunities")]
        [HttpGet]
        public IEnumerable<OpportunityDto> getMyAppliedOpportunities(long userId)
        {
            return _opportunityBll.getMyAppliedOpportunities(userId);
        }

        [Route("getRespondedUsersForMyOpportunities")]
        [HttpGet]
        public IEnumerable<OpportunityResponseDto> getRespondedUsersForMyOpportunities(long userId)
        {
            return _opportunityBll.getRespondedUsersForMyOpportunities(userId);
        }

        [Route("createOpportunity")]
        [HttpPost]
        public void createOpportunity(OpportunityDto opportunity)
        {
            _opportunityBll.createOpportunity(opportunity);
        }

        [Route("updateOpportunity")]
        [HttpPost]
        public void updateOpportunity(OpportunityDto opportunity)
        {
            _opportunityBll.updateOpportunity(opportunity);
        }

        [Route("DeleteOpportunity")]
        [HttpGet]
        public void deleteOpportunity(int opportunityId)
        {
            _opportunityBll.deleteOpportunity(opportunityId);
        }

        [Route("ApplyOpportunity")]
        [HttpPost]
        public bool ApplyOpportunity(ApplyOpportunityDto opportunity)
        {
           return _opportunityBll.applyOpportunity(opportunity);
        }

        [Route("AllOpportunitesForApproval")]
        [HttpGet]
        public IEnumerable<OpportunityDto> AllOpportunitesForApproval()
        {
            return _opportunityBll.AllOpportunitesForApproval();
        }

        [Route("ApproveRejectOpportunity")]
        [HttpGet]
        public bool ApproveOrRejectOpportunity(long IsApprovedBy, int OpportunityId, bool IsApproved, string IsApprovedComments)
        {
            return _opportunityBll.ApproveOrRejectOpportunity(IsApprovedBy, OpportunityId, IsApproved, IsApprovedComments);
        }
    }
}
