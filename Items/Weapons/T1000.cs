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
	public class T1000 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("T1000");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 370;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 20;
	        Item.height = 12;
	        Item.useTime = 12;
	        Item.useAnimation = 12;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 4f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("T1000").Type;
	        Item.shootSpeed = 24f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Purge");
			recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddIngredient(null, "CosmiliteBar", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("T1000").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}