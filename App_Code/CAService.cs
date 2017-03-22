using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CAService" in code, svc and config file together.
public class CAService : ICAService
{
    Community_AssistEntities CommAssEnt = new Community_AssistEntities();

    public bool ApplyForGrant(GrantRequest gr)
    {
        bool result = true;
        try
        {
            GrantReview review = new GrantReview();
            review.GrantRequest= gr;
            review.GrantRequestStatus = "pending";
            CommAssEnt.GrantRequests.Add(gr);
            //CommAssEnt.GrantReviews.Add(review);
            CommAssEnt.SaveChanges();
        }
        catch(Exception ex)
        {
            
            result = false;
            throw ex;
        }
        return result;
    }

    public List<GrantInfo> GetGrantsByPerson(int personId)
    {
        var grants = from g in CommAssEnt.GrantRequests
                     from r in g.GrantReviews
                     where g.PersonKey == personId
                     select new
                     {
                         g.GrantRequestDate,
                         g.GrantRequestExplanation,
                         g.GrantType.GrantTypeName,
                         g.GrantRequestAmount,
                         r.GrantRequestStatus

                     };
        List<GrantInfo> info = new List<GrantInfo>();
        foreach(var gr in grants)
        {
            GrantInfo gi = new GrantInfo();
            gi.GrantTypeName = gr.GrantTypeName;
            gi.GrantRequestExplanation = gr.GrantRequestExplanation;
            gi.GrantRequestAmount = (decimal)gr.GrantRequestAmount;
            gi.GrantRequestStatus = gr.GrantRequestStatus;

            info.Add(gi);
        }
        return info;

    }

    public List<GrantType> GetGrantTypes()
    {
        var types = from g in CommAssEnt.GrantTypes
                     select new
                     {
                         g.GrantTypeKey,
                         g.GrantTypeName,
                     };
        List<GrantType> gTypes = new List<GrantType>();
        foreach(var t in types)
        {
            GrantType gt = new GrantType();
            gt.GrantTypeKey = t.GrantTypeKey;
            gt.GrantTypeName = t.GrantTypeName;
            gTypes.Add(gt);
        }
        return gTypes;
    }

    public int PersonLogin(string user, string password)
    {
        int result = CommAssEnt.usp_Login(user, password);
        int key = 0;
        if(result != -1)
        {
            var uKey = (from p in CommAssEnt.People
                        where p.PersonEmail.Equals(user)
                        select p.PersonKey).FirstOrDefault();
            key = (int)uKey;
        }
        
        return key;
    }

    public bool RegisterPerson(PersonLite p)
    {
        bool result = true;
        int successResult = CommAssEnt.usp_Register(p.LastName, p.FirstName, p.Email, p.Password, 
            p.ApartmentNumber, p.Street, p.City, p.State, p.ZipCode, p.HomePhone, p.WorkPhone);
        if(successResult == -1)
        {
            result = false;
        }
            return result;
        
    }
}
