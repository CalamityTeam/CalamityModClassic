using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Triploon : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Triploon");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Triploon");
        Item.damage = 84;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 46;
        Item.height = 24;
        ////Tooltip.SetDefault("Fires three harpoons");
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7.5f;
        Item.value = 800000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item10;
        Item.autoReuse = true;
        Item.shootSpeed = 20f;
        Item.shoot = Mod.Find<ModProjectile>("Triploon").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num117 = 0.314159274f;
		int num118 = 3;
		Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
		vector7.Normalize();
		vector7 *= 30f;
		bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
		for (int num119 = 0; num119 < num118; num119++)
		{
			float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
			Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
			if (!flag11)
			{
				value9 -= vector7;
			}
			Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
		}
		return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "Dualpoon");
        recipe.AddIngredient(ItemID.Harpoon);
        recipe.AddIngredient(ItemID.Ectoplasm, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}