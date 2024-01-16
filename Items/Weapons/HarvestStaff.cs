using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class HarvestStaff : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
		{
			//texture =("CalamityModClassic1Point1/Items/Weapons/HarvestStaff");
			return true;
		}

		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Harvest Staff");
			Item.damage = 15;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 5;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = 5;
			Item.staff[Item.type] = true;
			////Tooltip.SetDefault("Casts flaming pumpkins");
			Item.noMelee = true; 
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("FlamingPumpkin").Type;
			Item.shootSpeed = 10f;
			Item.scale = 0.9f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Pumpkin, 20);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}