using System.ComponentModel;

namespace ClientService.Domain.Enums;

public enum NotificationType
{
    [Description("A freelancer has been register. Let's check and approve his/her account")] FreelancerCreated,
    
    [Description("You freelancer account has been approved. Let's check and use our service")] FreelancerAccepted,

    [Description("A post has been created. Let's check and support ít")] PostCreated,
    
    [Description("A transaction has been created. Let's check")] TransactionCreated,
    
    [Description("Your transaction has been confirm. Waiting freelancer contact to support")] TransactionConfirmed,
    
    [Description("Your transaction has been canceled. Let's check again")] TransactionCanceled,

    [Description("You have new message in a post. Let's check")] MessageCreated
}