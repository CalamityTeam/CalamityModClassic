using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.PlaguebringerGoliath;
using CalamityModClassic1Point2.Items.Weapons.Plaguebringer;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Weapons.Polterghast;

namespace CalamityModClassic1Point2.Items.Polterghast
{
	public class PolterghastBag : ModItem
	{
		public override void SetStaticDefaults()
 		{
 			//DisplayName.SetDefault("Treasure Bag");
 			//Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
 		}
		
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Cyan;
			Item.expert = true;
			//bossBagNPC = Mod.Find<ModNPC>("Polterghast").Type;
		}

		public override bool CanRightClick()
		{
			return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Affliction>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<RuinousSoul>(), 1, 15, 20));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BansheeHook>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EtherealSubjugator>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DaemonsFlame>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<FatesReveal>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<GhastlyVisage>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<GhoulishGouger>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TerrorBlade>(), 3));
        }
	}
}