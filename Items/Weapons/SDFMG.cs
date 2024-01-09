using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SDFMG : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("SDFMG");
		//Tooltip.SetDefault("It came from the edge of Terraria\n50% chance to not consume ammo");
	}

    public override void SetDefaults()
    {
        Item.damage = 129;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 66;
        Item.height = 32;
        Item.crit += 16;
        Item.useTime = 2;
        Item.useAnimation = 4;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2.75f;
        Item.value = 3000000;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 16f;
        Item.useAmmo = 97;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-5, 6) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-5, 6) * 0.05f;
	    if (Main.rand.NextBool(5))
	    {
	    	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("FishronRPG").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    }
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 50)
    		return false;
    	return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SDMG);
        recipe.AddIngredient(ItemID.ShrimpyTruffle);
        recipe.AddIngredient(null, "CosmiliteBar", 4);
        recipe.AddIngredient(null, "Phantoplasm", 4);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}