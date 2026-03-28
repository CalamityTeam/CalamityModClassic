using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class GreatswordofJudgement : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Greatsword of Judgement");
			/* Tooltip.SetDefault("A pale white sword from a forgotten land\n" +
			                   "You can hear faint yet comforting whispers emanating from the blade\n" +
					            "'No matter where you may be you are never alone.\n" +
					            "I shall always be at your side, my lord'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 70;
			Item.damage = 58;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.useTime = 18;
			Item.useTurn = true;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 72;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("Judgement").Type;
			Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 11);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}
