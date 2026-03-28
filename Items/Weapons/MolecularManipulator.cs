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
	public class MolecularManipulator : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Molecular Manipulator");
			// Tooltip.SetDefault("Is it nullable or not?  Let's find out!\nFires a fast null bullet that distorts NPC stats\nUses your life as ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 700;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 60;
	        Item.height = 30;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 8f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shootSpeed = 25f;
	        Item.shoot = Mod.Find<ModProjectile>("NullShot2").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	player.statLife -= 5;
			if (player.statLife <= 0)
			{
				player.KillMe(PlayerDeathReason.ByOther(10), 1000.0, 0, false);
			}
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("NullShot2").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "NullificationRifle");
            recipe.AddIngredient(null, "DarkPlasma", 2);
            recipe.AddIngredient(null, "CoreofCalamity", 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}