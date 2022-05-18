namespace VotingModels;

public enum RegistrarActions
{
    Register,
    SoftDelete,
    CheckVoterData,
    DeleteLastRecord,
    RestoreSoftDeletedVoter,
    RestorePass,
    VotersList,
    ReplaceVoterData
    
// <option value="0">Реєстрація виборця</option>
//     <option value="1">Анулювання виборця</option>
//     <option value="2">Перевірка даних виборця</option>
//     <option value="3">Знищення останнього(хибного) запису</option>
//     <option value="4">Відновлення анульованого виборця</option>
//     <option value="5">Відновлення паролю виборця</option>
//     <option value="6">Список виборців</option>
//     <option value="7">Заміна даних виборця</option>
}