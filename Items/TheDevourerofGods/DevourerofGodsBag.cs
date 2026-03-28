using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons;
using CalamityModClassicPreTrailer.Items.Weapons.DevourerofGods;
using CalamityModClassicPreTrailer.Items.Weapons.RareVariants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.TheDevourerofGods
{
	public class DevourerofGodsBag : ModItem
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

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Vector2 origin = new Vector2(18f, 20f); //18, 17
			spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/TheDevourerofGods/DevourerofGodsBagGlow").Value, Item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
			LeadingConditionRule deathActive = new LeadingConditionRule(new RevCondition());
			if (CalamityWorldPreTrailer.revenge)
			{
				Player player = Main.LocalPlayer;
					itemLoot.Add(revActive.OnSuccess(ItemDropRule.ByCondition(new FVodkaCondition(), ModContent.ItemType<Fabsol>(), 1)));
				
				if (CalamityWorldPreTrailer.death && player.difficulty == 2)
					itemLoot.Add(revActive.OnSuccess(deathActive.OnSuccess(ItemDropRule.ByCondition(new HardcorePlayerCondition(), ModContent.ItemType<CosmicPlushie>(), 1))));
				itemLoot.Add(revActive.OnSuccess(ItemDropRule.ByCondition(new DefiledCondition(), ModContent.ItemType<CosmicDischarge>(), 20)));
				itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<CosmicDischarge>(), 100)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			}
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Norfleet>(), 40));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Skullmasher>(), 40));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DeathhailStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DevourerofGodsMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Eradicator>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Excelsus>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<TheObliterator>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EradicatorMelee>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Deathwind>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<StaffoftheMechworm>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CosmiliteBar>(), 1, 30, 40));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<NebulousCore>(), 1));
		}
	}
}