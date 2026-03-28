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
	public class CursedCapper : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cursed Capper");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 24;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 44;
	        Item.height = 32;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2.25f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item41;
	        Item.autoReuse = true;
	        Item.shootSpeed = 14f;
	        Item.shoot = Mod.Find<ModProjectile>("CursedRound").Type;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("CursedRound").Type, damage, knockback, player.whoAmI, 0f, 0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.PhoenixBlaster);
	        recipe.AddIngredient(ItemID.CursedFlame, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}