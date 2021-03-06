using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Services
{
    public interface IItemService
    {
        void AffectGearItemIds(IGear gear, int itemId);

        void MakeGearChanges(int characterId, int itemId);

        void DropItemFromGear(int itemId, int characterId);

        void DropItemFromInventory(int itemId, int characterId);

        void SellItem(int itemId, int characterId);

        void BuyItem(int itemId, int characterId);

        void PickItem(int itemId, int characterId);

        void PutHandItemOn(int itemId, int characterId);

        void PutArmorOn(int itemId, int characterId);

        IItem GetItemById(int itemId);

        List<IItemViewModel> GetItemViewModelsForGear(int characterId);

        List<IItemViewModel> GetItemViewModelsForInventory(int characterId);

        void GiveCharacterHisBasicGear(int characterId);

        int GetItemIdByItsName(string itemName);

        IItemViewModel InitializeItemBonuses(int itemId);

        List<string> GetItemDroppingFactors();

        IEnumerable<T> GetEnumerableOfType<T>() where T : class;
    }
}
