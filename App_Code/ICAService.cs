using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICAService" in both code and config file together.
[ServiceContract]
public interface ICAService
{
    [OperationContract]
    int PersonLogin(string user, string password);

    [OperationContract]
    bool RegisterPerson(PersonLite p);

    [OperationContract]
    bool ApplyForGrant(GrantRequest gr);

    [OperationContract]
    List <GrantType> GetGrantTypes();

    [OperationContract]
    List<GrantInfo> GetGrantsByPerson(int personId);
}

[DataContract]
public class PersonLite
{//from StoredProcedures table, usp_Register members
    [DataMember]
    public string LastName { set; get; }

    [DataMember]
    public string FirstName { set; get; }

    [DataMember]
    public string Email { set; get; }

    [DataMember]
    public string Password { set; get; }

    [DataMember]
    public string ApartmentNumber { set; get; }

    [DataMember]
    public string Street { set; get; }

    [DataMember]
    public string City { set; get; }

    [DataMember]
    public string State { set; get; }

    [DataMember]
    public string ZipCode { set; get; }

    [DataMember]
    public string HomePhone { set; get; }

    [DataMember]
    public string WorkPhone { set; get; }

}

[DataContract]
public class GrantInfo
{
    [DataMember]//from GrantRequest table
    public string GrantTypeName { set; get; }

    [DataMember]//from GrantRequest table
    public string GrantRequestExplanation { set; get; }

    [DataMember]//from GrantRequest table
    public decimal GrantRequestAmount { set; get; }

    [DataMember]//from GrantReview table
    public string GrantRequestStatus { set; get; }

}
