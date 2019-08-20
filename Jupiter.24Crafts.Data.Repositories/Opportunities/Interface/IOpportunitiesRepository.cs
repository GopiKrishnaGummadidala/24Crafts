using Jupiter._24Crafts.Data.Dtos.Opportunities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Opportunities.Interface
{
   public interface IOpportunitiesRepository
    {
      IEnumerable<OpportunityDto> searchOpportunities(SearchOpportunityRequestDto searchObj);
      bool  createOpportunity(OpportunityDto opportunity);
      bool updateOpportunity(OpportunityDto opportunity);
      bool applyOpportunity(ApplyOpportunityDto opportunity);
      bool deleteOpportunity(int opportunityId);
      IEnumerable<OpportunityDto> getMyOpportunities(long userId);
      IEnumerable<OpportunityDto> getMyAppliedOpportunities(long userId);
      IEnumerable<OpportunityDto> getOpportunities(long userId);
      IEnumerable<OpportunityResponseDto> getRespondedUsersForMyOpportunities(long userId);
      IEnumerable<OpportunityDto> AllOpportunitesForApproval();
      bool ApproveOrRejectOpportunity(long IsApprovedBy, int OpportunityId, bool IsApproved, string IsApprovedComments);
      int getOpportunitiesNotification(long userId);
    }
}
