using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class MagnaStriker : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magna Striker");
			// Tooltip.SetDefault("Fires a string of opal and magna strikes");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 42;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 60;
			Item.height = 22;
			Item.useTime = 5;
			Item.reuseDelay = 6;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2.25f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/OpalStrike");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("OpalStrike").Type;
			Item.shootSpeed = 15f;
			Item.useAmmo = 97;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int randomProj = Main.rand.Next(2);
			if (randomProj == 0)
			{
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("OpalStrike").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
			else
			{
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("MagnaStrike").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "OpalStriker");
            recipe.AddIngredient(null, "MagnaCannon");
            recipe.AddIngredient(ItemID.AdamantiteBar, 6);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "OpalStriker");
            recipe.AddIngredient(null, "MagnaCannon");
            recipe.AddIngredient(ItemID.TitaniumBar, 6);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}