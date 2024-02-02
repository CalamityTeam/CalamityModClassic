using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Calamitas;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.TheDevourerofGods
{
	public class DevourerofGodsBag : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Treasure Bag");
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			////Tooltip.SetDefault("Right click to open");
			Item.rare = 9;
			Item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
        }


        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DevourerofGodsMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<NebulousCore>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CosmiliteBar>(), 1, 30, 39));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DeathhailStaff>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Eradicator>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Excelsus>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheObliterator>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EradicatorMelee>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Deathwind>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<StaffoftheMechworm>(), 10));
        }
	}
}