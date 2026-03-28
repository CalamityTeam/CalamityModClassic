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
	public class Phangasm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phangasm");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 112;
	        Item.width = 20;
	        Item.height = 12;
	        Item.useTime = 12;
	        Item.useAnimation = 12;
	        Item.useStyle = 5;
	        Item.knockBack = 3f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item5;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Ranged;
			Item.channel = true;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Phangasm").Type;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Phangasm").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Phantasm);
	        recipe.AddIngredient(null, "CosmiliteBar", 5);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}