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
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DevourerofGodsMask>(), 7));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<NebulousCore>(), 1));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CosmiliteBar>(), 1, 30, 39));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DeathhailStaff>(), 10));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Eradicator>(), 10));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Excelsus>(), 10));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TheObliterator>(), 10));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EradicatorMelee>(), 10));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Deathwind>(), 10));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<StaffoftheMechworm>(), 10));
        }
	}
}