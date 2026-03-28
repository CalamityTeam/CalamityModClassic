using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.HiveMind
{
	public class Shadethrower : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shadethrower");
			// Tooltip.SetDefault("33% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 54;
			Item.height = 14;
			Item.useTime = 10;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ShadeFire").Type;
			Item.shootSpeed = 5.5f;
			Item.useAmmo = 23;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 33)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.RottenChunk, 3);
	        recipe.AddIngredient(ItemID.DemoniteBar, 7);
	        recipe.AddIngredient(null, "TrueShadowScale", 10);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
	    }
	}
}