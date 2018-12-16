namespace PANDA.Serv.Contracts
{
    using System.Collections.Generic;
    using DataModels;
    using DataModels.Enums;
    using SIS.Framework.Security;
    using ViewModels;

    public interface IPackagesService
    {
        IEnumerable<Package> GetAllPackages(IIdentity user);

        IEnumerable<Package> GetAllPackagesByStatus(PackageStatus status);

        Package GetPackageById(IIdentity user, int id);

        void CreatePackage(PackageBinidngModel model, int pecipientId);

        void ChangePackageStatus(int packageId, PackageStatus status);
    }
}