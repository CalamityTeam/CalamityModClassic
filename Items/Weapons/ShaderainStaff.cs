using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ShaderainStaff : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/2.HiveMind/ShaderainStaff");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Shaderain Staff");
        Item.damage = 17;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 34;
        Item.height = 34;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 1;
        ////Tooltip.SetDefault("Fires a shade storm cloud");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 0f;
        Item.value = 40000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item66;
        Item.shoot = Mod.Find<ModProjectile>("ShadeNimbus").Type;
        Item.shootSpeed = 16f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	int i = Main.myPlayer;
		float num72 = Item.shootSpeed;
		int num73 = damage;
		float num74 = knockback;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
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
		int num154 = Projectile.NewProjectile(source, vector2.X, vector2.Y, num78, num79, Mod.Find<ModProjectile>("ShadeNimbusCloud").Type, num73, num74, i, 0f, 0f);
		Main.projectile[num154].ai[0] = (float)Main.mouseX + Main.screenPosition.X;
		Main.projectile[num154].ai[1] = (float)Main.mouseY + Main.screenPosition.Y;
    	return false;
	}
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RottenChunk, 2);
        recipe.AddIngredient(ItemID.DemoniteBar, 3);
        recipe.AddIngredient(null, "TrueShadowScale", 12);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}}