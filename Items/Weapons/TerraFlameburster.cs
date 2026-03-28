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
	public class TerraFlameburster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Flameburster");
			// Tooltip.SetDefault("80% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 56;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 68;
			Item.height = 22;
			Item.useTime = 3;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 3.25f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("TerraFireGreen").Type;
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 80)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Flamethrower);
	        recipe.AddIngredient(null, "LivingShard", 7);
	        recipe.AddIngredient(null, "EssenceofCinder", 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}