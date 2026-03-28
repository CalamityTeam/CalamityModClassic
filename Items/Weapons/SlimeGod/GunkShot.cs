using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.SlimeGod
{
	public class GunkShot : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gunk Shot");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 42;
			Item.height = 18;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 4;
			Item.UseSound = SoundID.Item36;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 3.5f;
			Item.useAmmo = 97;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    int num6 = Main.rand.Next(3, 5);
		    for (int index = 0; index < num6; ++index)
		    {
		        float SpeedX = velocity.X + (float) Main.rand.Next(-25, 26) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-25, 26) * 0.05f;
		        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "PurifiedGel", 18);
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
		}
	}
}