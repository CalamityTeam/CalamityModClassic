using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Accessories.Wings;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Patreon;
using CalamityModClassicPreTrailer.Items.Weapons.Yharon;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Yharon
{
	public class YharonBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Treasure Bag");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 9;
			Item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DrewsWings>(), 1));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<FoxDrive>(), 1));
			itemLoot.Add(revActive.OnSuccess(new OneFromOptionsDropRule(20, 1, new int[]
			{
				ModContent.ItemType<StressPills>(),
				ModContent.ItemType<Laudanum>(),
				ModContent.ItemType<HeartofDarkness>(), 
			})));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ForgottenDragonEgg>(), 10));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<AngryChickenStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DragonsBreath>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PhoenixFlameBarrage>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DragonRage>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ProfanedTrident>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<TheBurningSky>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ChickenCannon>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<YharonMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<YharimsGift>(), 1));
		}
	}
}