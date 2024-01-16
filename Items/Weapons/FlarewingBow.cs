using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FlarewingBow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/FlarewingBow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Flarewing Bow");
        Item.damage = 26;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 30;
        Item.height = 92;
        Item.useTime = 33;
        Item.useAnimation = 33;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Converts wooden arrows into flaming obsidian bats");
        Item.knockBack = 1.5f;
        Item.value = 200000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 1; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num117 = 0.314159274f;
		int num118 = 5;
		Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
		vector7.Normalize();
		vector7 *= 50f;
		bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
		for (int num119 = 0; num119 < num118; num119++)
		{
			float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
			Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
			if (!flag11)
			{
				value9 -= vector7;
			}
			int num122 = type;
			if (num122 == ProjectileID.WoodenArrowFriendly)
			{
				int num123 = Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("FlareBat").Type, (int)((double)damage * 1.25f), knockback, player.whoAmI, 0.0f, 0.0f);
				Main.projectile[num123].noDropItem = true;
			}
			else
			{
				int num123 = Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, (int)((double)damage * 0.5f), knockback, player.whoAmI, 0.0f, 0.0f);
				Main.projectile[num123].noDropItem = true;
			}
		}
		return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HellwingBow);
        recipe.AddIngredient(null, "EssenceofCinder", 5);
        recipe.AddIngredient(ItemID.LivingFireBlock, 50);
        recipe.AddIngredient(ItemID.Obsidian, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}