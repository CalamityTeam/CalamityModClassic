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
	public class TheEmpyrean : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Empyrean");
			// Tooltip.SetDefault("70% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 82;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 70;
			Item.height = 24;
			Item.useTime = 5;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CosmicFire").Type;
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 70)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "MeldiateBar", 12);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}