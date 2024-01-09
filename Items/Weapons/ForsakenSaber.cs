using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class ForsakenSaber : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Forsaken Saber");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.damage = 55;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 23;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 23;
			Item.useTurn = true;
			Item.knockBack = 6;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 48;
			Item.maxStack = 1;
			Item.value = 350000;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("SandBlade").Type;
			Item.shootSpeed = 5f;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
			recipe.AddIngredient(ItemID.AdamantiteBar, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
			recipe.AddIngredient(ItemID.TitaniumBar, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Teleporter);
	        }
	    }
	}
}
