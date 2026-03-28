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
	public class HellBurst : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hell Burst");
			// Tooltip.SetDefault("Casts a beam of flame");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 40;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 14;
	        Item.width = 52;
	        Item.height = 52;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = 1;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item34;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FlameBeamTip").Type;
	        Item.shootSpeed = 32f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Flamelash);
	        recipe.AddIngredient(ItemID.CrystalVileShard);
	        recipe.AddIngredient(ItemID.DarkShard, 2);
	        recipe.AddIngredient(ItemID.SoulofNight, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}