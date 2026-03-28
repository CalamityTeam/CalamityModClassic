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
	public class MagnaCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magna Cannon");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 19;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 12;
	        Item.width = 56;
	        Item.height = 34;
	        Item.useTime = 32;
	        Item.useAnimation = 32;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
	        Item.UseSound = SoundID.Item117;
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("MagnaBlast").Type;
	    }

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        int num6 = 3;
	        for (int index = 0; index < num6; ++index)
	        {
	            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
	        return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Granite, 25);
	        recipe.AddIngredient(ItemID.Obsidian, 15);
	        recipe.AddIngredient(ItemID.Amber, 5);
	        recipe.AddIngredient(ItemID.SpaceGun);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}