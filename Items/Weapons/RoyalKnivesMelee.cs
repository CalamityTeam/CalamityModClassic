using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class RoyalKnivesMelee : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Illustrious Knives");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.damage = 255;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 12;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 12;
		Item.knockBack = 3f;
		Item.UseSound = SoundID.Item39;
		Item.autoReuse = true;
		Item.height = 20;
		Item.maxStack = 1;
		Item.value = 8000000;
		Item.shoot = Mod.Find<ModProjectile>("RoyalKnifeMelee").Type;
		Item.shootSpeed = 9f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(255, 0, 255);
            }
        }
    }
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
		float num72 = Item.shootSpeed;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default(Vector2));
		Vector2 vector3 = Main.MouseWorld - vector2;
    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
		if (player.gravDir == -1f)
		{
			num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
		}
		float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
		float num81 = num80;
		if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
		{
			num78 = (float)player.direction;
			num79 = 0f;
			num80 = num72;
		}
		else
		{
			num80 = num72 / num80;
		}
    	num78 *= num80;
		num79 *= num80;
		int num146 = 4;
		if (Main.rand.NextBool(2))
		{
			num146++;
		}
		if (Main.rand.NextBool(4))
		{
			num146++;
		}
		if (Main.rand.NextBool(8))
		{
			num146++;
		}
		if (Main.rand.NextBool(16))
		{
			num146++;
		}
		for (int num147 = 0; num147 < num146; num147++)
		{
			float num148 = num78;
			float num149 = num79;
			float num150 = 0.05f * (float)num147;
			num148 += (float)Main.rand.Next(-35, 36) * num150;
			num149 += (float)Main.rand.Next(-35, 36) * num150;
			num80 = (float)Math.Sqrt((double)(num148 * num148 + num149 * num149));
			num80 = num72 / num80;
			num148 *= num80;
			num149 *= num80;
			float x4 = vector2.X;
			float y4 = vector2.Y;
			Projectile.NewProjectile(source, x4, y4, num148, num149, Mod.Find<ModProjectile>("RoyalKnifeMelee").Type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddIngredient(null, "CoreofCalamity", 3);
        recipe.AddIngredient(ItemID.VampireKnives);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}
