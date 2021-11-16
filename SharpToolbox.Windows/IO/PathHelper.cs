using System;
using System.Runtime.InteropServices;
using SharpToolbox.Safes;
using SharpToolbox.Windows.Native;

namespace SharpToolbox.Windows.IO;

public static class PathHelper
{
    private static readonly string[] KnownFolderGuids =
    {
        "{de61d971-5ebc-4f02-a3a9-6c82895e5c04}", // AddNewPrograms 
        "{724EF170-A42D-4FEF-9F26-B60E846FBA4F}", // AdminTools 
        "{a305ce99-f527-492b-8b1a-7e76fa98d6e4}", // AppUpdates 
        "{9E52AB10-F80D-49DF-ACB8-4330F5687855}", // CDBurning 
        "{df7266ac-9274-4867-8d55-3bd661de872d}", // ChangeRemovePrograms 
        "{D0384E7D-BAC3-4797-8F14-CBA229B392B5}", // CommonAdminTools 
        "{C1BAE2D0-10DF-4334-BEDD-7AA20B227A9D}", // CommonOEMLinks 
        "{0139D44E-6AFE-49F2-8690-3DAFCAE6FFB8}", // CommonPrograms 
        "{A4115719-D62E-491D-AA7C-E74B8BE3B067}", // CommonStartMenu 
        "{82A5EA35-D9CD-47C5-9629-E15D2F714E6E}", // CommonStartup 
        "{B94237E7-57AC-4347-9151-B08C6C32D1F7}", // CommonTemplates 
        "{0AC0837C-BBF8-452A-850D-79D08E667CA7}", // ComputerFolder 
        "{4bfefb45-347d-4006-a5be-ac0cb0567192}", // ConflictFolder 
        "{6F0CD92B-2E97-45D1-88FF-B0D186B8DEDD}", // ConnectionsFolder 
        "{56784854-C6CB-462b-8169-88E350ACB882}", // Contacts 
        "{82A74AEB-AEB4-465C-A014-D097EE346D63}", // ControlPanelFolder 
        "{2B0F765D-C0E9-4171-908E-08A611B84FF6}", // Cookies 
        "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}", // Desktop 
        "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}", // Documents 
        "{374DE290-123F-4565-9164-39C4925E467B}", // Downloads 
        "{1777F761-68AD-4D8A-87BD-30B759FA33DD}", // Favorites 
        "{FD228CB7-AE11-4AE3-864C-16F3910AB8FE}", // Fonts 
        "{CAC52C1A-B53D-4edc-92D7-6B2E8AC19434}", // Games 
        "{054FAE61-4DD8-4787-80B6-090220C4B700}", // GameTasks 
        "{D9DC8A3B-B784-432E-A781-5A1130A75963}", // History 
        "{352481E8-33BE-4251-BA85-6007CAEDCF9D}", // InternetCache 
        "{4D9F7874-4E0C-4904-967B-40B0D20C3E4B}", // InternetFolder 
        "{bfb9d5e0-c6a9-404c-b2b2-ae6db6af4968}", // Links 
        "{F1B32785-6FBA-4FCF-9D55-7B8E7F157091}", // LocalAppData 
        "{A520A1A4-1780-4FF6-BD18-167343C5AF16}", // LocalAppDataLow 
        "{2A00375E-224C-49DE-B8D1-440DF7EF3DDC}", // LocalizedResourcesDir 
        "{4BD8D571-6D19-48D3-BE97-422220080E43}", // Music 
        "{C5ABBF53-E17F-4121-8900-86626FC2C973}", // NetHood 
        "{D20BEEC4-5CA8-4905-AE3B-BF251EA09B53}", // NetworkFolder 
        "{2C36C0AA-5812-4b87-BFD0-4CD0DFB19B39}", // OriginalImages 
        "{69D2CF90-FC33-4FB7-9A0C-EBB0F0FCB43C}", // PhotoAlbums 
        "{33E28130-4E1E-4676-835A-98395C3BC3BB}", // Pictures 
        "{DE92C1C7-837F-4F69-A3BB-86E631204A23}", // Playlists 
        "{76FC4E2D-D6AD-4519-A663-37BD56068185}", // PrintersFolder 
        "{9274BD8D-CFD1-41C3-B35E-B13F55A758F4}", // PrintHood 
        "{5E6C858F-0E22-4760-9AFE-EA3317B67173}", // Profile 
        "{62AB5D82-FDC1-4DC3-A9DD-070D1D495D97}", // ProgramData 
        "{905e63b6-c1bf-494e-b29c-65b732d3d21a}", // ProgramFiles 
        "{6D809377-6AF0-444b-8957-A3773F02200E}", // ProgramFilesX64 
        "{7C5A40EF-A0FB-4BFC-874A-C0F2E0B9FA8E}", // ProgramFilesX86 
        "{F7F1ED05-9F6D-47A2-AAAE-29D317C6F066}", // ProgramFilesCommon 
        "{6365D5A7-0F0D-45E5-87F6-0DA56B6A4F7D}", // ProgramFilesCommonX64 
        "{DE974D24-D9C6-4D3E-BF91-F4455120B917}", // ProgramFilesCommonX86 
        "{A77F5D77-2E2B-44C3-A6A2-ABA601054A51}", // Programs 
        "{DFDF76A2-C82A-4D63-906A-5644AC457385}", // Public 
        "{C4AA340D-F20F-4863-AFEF-F87EF2E6BA25}", // PublicDesktop 
        "{ED4824AF-DCE4-45A8-81E2-FC7965083634}", // PublicDocuments 
        "{3D644C9B-1FB8-4f30-9B45-F670235F79C0}", // PublicDownloads 
        "{DEBF2536-E1A8-4c59-B6A2-414586476AEA}", // PublicGameTasks 
        "{3214FAB5-9757-4298-BB61-92A9DEAA44FF}", // PublicMusic 
        "{B6EBFB86-6907-413C-9AF7-4FC2ABF07CC5}", // PublicPictures 
        "{2400183A-6185-49FB-A2D8-4A392A602BA3}", // PublicVideos 
        "{52a4f021-7b75-48a9-9f6b-4b87a210bc8f}", // QuickLaunch 
        "{AE50C081-EBD2-438A-8655-8A092E34987A}", // Recent 
        "{B7534046-3ECB-4C18-BE4E-64CD4CB7D6AC}", // RecycleBinFolder 
        "{8AD10C31-2ADB-4296-A8F7-E4701232C972}", // ResourceDir 
        "{3EB685DB-65F9-4CF6-A03A-E3EF65729F3D}", // RoamingAppData 
        "{B250C668-F57D-4EE1-A63C-290EE7D1AA1F}", // SampleMusic 
        "{C4900540-2379-4C75-844B-64E6FAF8716B}", // SamplePictures 
        "{15CA69B3-30EE-49C1-ACE1-6B5EC372AFB5}", // SamplePlaylists 
        "{859EAD94-2E85-48AD-A71A-0969CB56A6CD}", // SampleVideos 
        "{4C5C32FF-BB9D-43b0-B5B4-2D72E54EAAA4}", // SavedGames 
        "{7d1d3a04-debb-4115-95cf-2f29da2920da}", // SavedSearches 
        "{ee32e446-31ca-4aba-814f-a5ebd2fd6d5e}", // SEARCH_CSC 
        "{98ec0e18-2098-4d44-8644-66979315a281}", // SEARCH_MAPI 
        "{190337d1-b8ca-4121-a639-6d472d16972a}", // SearchHome 
        "{8983036C-27C0-404B-8F08-102D10DCFD74}", // SendTo 
        "{7B396E54-9EC5-4300-BE0A-2482EBAE1A26}", // SidebarDefaultParts 
        "{A75D362E-50FC-4fb7-AC2C-A8BEAA314493}", // SidebarParts 
        "{625B53C3-AB48-4EC1-BA1F-A1EF4146FC19}", // StartMenu 
        "{B97D20BB-F46A-4C97-BA10-5E3608430854}", // Startup 
        "{43668BF8-C14E-49B2-97C9-747784D784B7}", // SyncManagerFolder 
        "{289a9a43-be44-4057-a41b-587a76d7e7f9}", // SyncResultsFolder 
        "{0F214138-B1D3-4a90-BBA9-27CBC0C5389A}", // SyncSetupFolder 
        "{1AC14E77-02E7-4E5D-B744-2EB1AE5198B7}", // System 
        "{D65231B0-B2F1-4857-A4CE-A8E7C6EA7D27}", // SystemX86 
        "{A63293E8-664E-48DB-A079-DF759E0509F7}", // Templates 
        "{5b3749ad-b49f-49c1-83eb-15370fbd4882}", // TreeProperties 
        "{0762D272-C50A-4BB0-A382-697DCD729B80}", // UserProfiles 
        "{f3ce0f7c-4901-4acc-8648-d5d44b04ef8f}", // UsersFiles 
        "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}", // Videos 
        "{F38BF404-1D43-42F2-9305-67DE0B28FC23}", // Windows 
    };

    /// <summary>
    /// Gets the current path to the specified known folder as currently configured. This does not require the folder to be existent.
    /// </summary>
    /// <param name="knownFolder">The known folder which current path will be returned.</param>
    /// <returns>The default path of the known folder.</returns>
    /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path could not be retrieved.</exception>
    public static string GetPath(KnownFolder knownFolder)
    {
        return GetPath(knownFolder, false);
    }

    /// <summary>
    /// Gets the current path to the specified known folder as currently configured. This does not require the folder to be existent.
    /// </summary>
    /// <param name="knownFolder">The known folder which current path will be returned.</param>
    /// <returns><see cref="true"/> if a valid path has been found otherwise <see cref="false"/></returns>
    public static bool TryGetPath(KnownFolder knownFolder, out string path)
    {
        return TryGetPath(knownFolder, out path, false);
    }

    /// <summary>
    /// Gets the current path to the specified known folder as currently configured. This does not require the folder to be existent.
    /// </summary>
    /// <param name="knownFolder">The known folder which current path will be returned.</param>
    /// <param name="defaultUser">Specifies if the paths of the default user (user profile template) will be used. This requires administrative rights.</param>
    /// <returns>The default path of the known folder.</returns>
    /// <exception cref="ExternalException">Thrown if the path could not be retrieved.</exception>
    public static string GetPath(KnownFolder knownFolder, bool defaultUser)
    {
        return GetPath(knownFolder, KnownFolderFlags.DontVerify, defaultUser);
    }

    /// <summary>
    /// Gets the current path to the specified known folder as currently configured. This does not require the folder to be existent.
    /// </summary>
    /// <param name="knownFolder">The known folder which current path will be returned.</param>
    /// <param name="defaultUser">Specifies if the paths of the default user (user profile template) will be used. This requires administrative rights.</param>
    /// <returns><see cref="true"/> if a valid path has been found otherwise <see cref="false"/></returns>
    public static bool TryGetPath(KnownFolder knownFolder, out string path, bool defaultUser)
    {
        return TryGetPath(knownFolder, out path, KnownFolderFlags.DontVerify, defaultUser);
    }

    /// <summary>
    /// Gets the current path to the specified known folder as currently configured. This does not require the folder to be existent.
    /// </summary>
    /// <param name="knownFolder">Know Folder</param>
    /// <param name="flags">Folder Flags</param>
    /// <param name="defaultUser">Default user</param>
    /// <returns>The default path of the known folder.</returns>
    /// <exception cref="System.Runtime.InteropServices.ExternalException"></exception>
    private static string GetPath(KnownFolder knownFolder, KnownFolderFlags flags, bool defaultUser)
    {
        string path;
        if (TryGetPath(knownFolder, out path, flags, defaultUser))
            return path;
        throw new ExternalException($"Unable to retrieve the known folder ({knownFolder.ToString()}) path. It may not be available on this system.");
    }

    /// <summary>
    /// Gets the current path to the specified known folder as currently configured. This does not require the folder to be existent.
    /// </summary>
    /// <param name="knownFolder">Know Folder</param>
    /// <param name="path">Out path</param>
    /// <param name="flags">Folder Flags</param>
    /// <param name="defaultUser">Default user</param>
    /// <returns>if a valid path has been found, false otherwise</returns>
    private static bool TryGetPath(KnownFolder knownFolder, out string path, KnownFolderFlags flags, bool defaultUser)
    {
        SafeResult<string> safe = Safe.Try(() =>
        {
            int result = Shell32.SHGetKnownFolderPath(new Guid(KnownFolderGuids[(int) knownFolder]), (uint) flags, new IntPtr(defaultUser ? -1 : 0),
                out IntPtr outPath);
            return result >= 0 ? Marshal.PtrToStringUni(outPath) : null;
        });
        path = safe.Result;
        return safe.IsSuccess && !string.IsNullOrWhiteSpace(safe.Result);
    }
}